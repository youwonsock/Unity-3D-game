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

    [Header ("Set in inspector")]
    [SerializeField] UiManager uiManager;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Player player;
    [SerializeField] private Boss boss;

    [Header ("Set in Script")]
    [SerializeField]private int score;
    [SerializeField] private int stage;
    [SerializeField] private float playTime;
    [SerializeField] private bool isBattle;
    [SerializeField] private int enemyCountA;
    [SerializeField] private int enemyCountB;
    [SerializeField] private int enemyCountC;

    private bool isGameStart;

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
