using System;
using System.Collections;
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

    private int score;
    private int stage;
    private float playTime;
    private bool isBattle;
    private int enemyCountA;
    private int enemyCountB;
    private int enemyCountC;
    private bool isGameStart;

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
     * @date last change 2022/08/11
     */
    public GameObject[] Items { get { return items; } }

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

        StartCoroutine(temp());
    }

    /**
     * @brief Stage 종료 시 호출되는 메서드
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    public void EndStage()
    {
        player.transform.position = Vector3.up * 0.8f;
        EndStageEvent();
        isBattle = false;


    }



    IEnumerator temp()
    {
        yield return new WaitForSecondsRealtime(5);

        EndStage();
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
            uiManager.SetBossUI(boss);
            uiManager.SetEnemyUI(enemyCountA, enemyCountB, enemyCountC);
            uiManager.SetUtilityUI();
        }
    }

    #endregion



    #region Unity Event

    private void Update()
    {
        playTime += Time.deltaTime;
    }

    private void OnEnable()
    {
        UpdateManager.SubscribeToLateUpdate(SetGameUI);
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromLateUpdate(SetGameUI);
    }

    #endregion
}
