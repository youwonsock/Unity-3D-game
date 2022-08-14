using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/**
 * @brief class GameManager 클래스
 * 
 * @author yws
 * @date last change 2022/08/11
 */
public class GameManager : Singleton<GameManager>
{
    public GameManager() { }


    #region Fields

    [Header ("in Game Objects")]
    [SerializeField] UiManager uiManager;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Player player;
    [SerializeField] private Boss boss;

    [Header ("Prefabs ")]
    [SerializeField] private Transform[] enemySpawnZones;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] items;

    [SerializeField]private int stage;
    private int score;
    private float playTime;
    private int enemyCountA;
    private int enemyCountB;
    private int enemyCountC;
    private bool isGameStart;
    private bool isBattle;
    private List<int> enemyIdxList = new List<int>();

    public event Action StartStageEvent;
    public event Action EndStageEvent;

    #endregion


    #region Property

    /**
     * @brief 현재 Score Getter\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public int Score 
    { 
        get 
        { 
            return score; 
        }
        set
        {
            score = Mathf.Clamp(value, 0, Constants.INF);
        }
    }

    /**
     * @brief 현재 Stage Getter\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public int Stage { get { return stage; } }

    /**
     * @brief 현재 PlayTime Getter\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public float PlayTime { get { return playTime; } }

    /**
     * @brief 게임 시작 여부 Getter\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public bool IsGameStart { get { return isGameStart; } }

    /**
     * @brief Enemy Drop Items Getter\n
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    public GameObject[] Items { get { return items; } }

    /**
     * @brief EnemyCountA Property\n
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    public int EnemyCountA { get { return enemyCountA; } set { enemyCountA = value; } }

    /**
     * @brief EnemyCountA Property\n
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    public int EnemyCountB { get { return enemyCountB; } set { enemyCountB = value; } }

    /**
     * @brief EnemyCountA Property\n
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    public int EnemyCountC { get { return enemyCountC; } set { enemyCountC = value; } }

    #endregion



    #region Funtion

    //--------------------------public--------------------------------------


    /**
     * @brief 게임 시작 메서드
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public void GameStart()
    {
        player.gameObject.SetActive(true);
        objectPool.gameObject.SetActive(true);
        uiManager.SetupGameUI();

        isGameStart = true;
    }

    /**
     * @brief 게임 재시작 메서드
     * 
     * @author yws
     * @date last change 2022/08/14
     */
    public void ReStartGame()
    {
        score = 0;
        stage = 0;
        playTime = 0;
        enemyCountA = 0;
        enemyCountB = 0;
        enemyCountC = 0;
        isGameStart = false;
        isBattle = false;

        enemyIdxList.Clear();
        player.InitPlayer();

        Enemy[] enemys = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in enemys)
            Destroy(enemy.gameObject);


        EndStage();
        uiManager.SetGameOverUI(false);
    }

    /**
     * @brief Stage 시작 시 호출되는 메서드
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    public void StartStage()
    {
        StartStageEvent();
        stage++;
        isBattle = true;

        for(int i = 0; i < enemySpawnZones.Length; i++)
            enemySpawnZones[i].gameObject.SetActive(true);

        StartCoroutine(CreateEnemy());
    }

    /**
     * @brief Stage 종료 시 호출되는 메서드
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    public void EndStage()
    {
        for (int i = 0; i < enemySpawnZones.Length; i++)
            enemySpawnZones[i].gameObject.SetActive(false);

        uiManager.SetBossUI(false);
        player.transform.position = Vector3.up * 0.8f;
        EndStageEvent();
    }

    /**
     * @brief GameOver 시 호출되는 메서드
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    public void GameOver()
    {
        uiManager.SetGameOverUI(true);

        //최고 점수 저장!
        try
        {
            if (score > PlayerPrefs.GetInt("MaxScore"))
                PlayerPrefs.SetInt("MaxScore", score);
        }
        catch (PlayerPrefsException e)
        {
            PlayerPrefs.SetInt("MaxScore", score);
        }
    }

    //--------------------------private--------------------------------------

    /**
     * @brief LateUpdate에서 호출되는 게임 UI 설정 메서드
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    private void SetGameUI()
    {
        if (isGameStart)
        {
            uiManager.SetPlayerUI(player);
            uiManager.SetBossHealthBar(boss);
            uiManager.SetEnemyUI(enemyCountA, enemyCountB, enemyCountC);
            uiManager.SetUtilityUI();
        }
    }

    /**
     * @brief Update 구독 메서드
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    private void OnUpdateWork()
    {
        //게임 시간 기록
        playTime += Time.deltaTime;

        // Stage 클리어 여부 확인
        if (isBattle && ((enemyCountA + enemyCountB + enemyCountC) == 0) && boss == null)
        {
            Invoke(nameof(EndStage), 5f);
            isBattle = false;
        }
    }

    /**
     * @brief Enemy 생성 Coroutine
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    IEnumerator CreateEnemy()
    {
        var wfs = new WaitForSecondsRealtime(1);

        // 생성할 Enemy를 Random으로 결정 후 Idx를 List에 삽입
        for (int i = 0; i < stage + 2; i++)
            enemyIdxList.Add(UnityEngine.Random.Range(0,3));

        //임시 Enemy Component 저장소
        Enemy enemy;

        // Enemy 생성 시작
        while(enemyIdxList.Count > 0)
        {
            int createZoneIdx = UnityEngine.Random.Range(0,4);

            // 생성 및 리스트에서 값 제거
            GameObject instantiateEnemy = Instantiate(enemies[enemyIdxList[0]], enemySpawnZones[createZoneIdx].position, Quaternion.identity);
            enemyIdxList.RemoveAt(0);

            // Component를 얻어온 후 값 설정
            instantiateEnemy.TryGetComponent<Enemy>(out enemy);
            enemy.Target = player.transform;

            switch (enemy.type)
            {
                case EnemyType.A:
                    enemyCountA++;
                    break;
                case EnemyType.B:
                    enemyCountB++;
                    break;
                case EnemyType.C:
                    enemyCountC++;
                    break;
            }

            yield return wfs;
        }

        // 5 스테이지마다 boss 생성
        if (stage % 5 == 0)
        {
            uiManager.SetBossUI(true);
            Instantiate(enemies[3], enemySpawnZones[2].position, Quaternion.identity).TryGetComponent<Boss>(out boss);
            boss.Target = player.transform;
        }
        
        yield break;
    }

    #endregion



    #region Unity Event

    private void OnEnable()
    {
        UpdateManager.SubscribeToLateUpdate(SetGameUI);
        UpdateManager.SubscribeToUpdate(OnUpdateWork);
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromLateUpdate(SetGameUI);
        UpdateManager.UnsubscribeFromUpdate(OnUpdateWork);
    }

    #endregion
}
