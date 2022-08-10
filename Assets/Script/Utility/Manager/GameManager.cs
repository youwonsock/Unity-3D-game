using UnityEngine;

/**
 * @brief class GameManager Ŭ����
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
     * @brief ���� Score Getter\n
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
     * @brief ���� Stage Getter\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public int Stage { get { return stage; } }

    /**
     * @brief ���� PlayTime Getter\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public float PlayTime { get { return playTime; } }

    /**
     * @brief ���� ���� ���� Getter\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public bool IsGameStart { get { return isGameStart; } }
    
    #endregion



    #region Funtion

    //--------------------------public--------------------------------------


    /**
     * @brief ���� ���� �޼���
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
     * @brief LateUpdate���� ȣ��Ǵ� ���� UI ���� �޼���
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
