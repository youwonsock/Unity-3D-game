using System;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief class UI 설정을 위한 Manager 클래스
 * 
 * @author yws
 * @date last change 2022/08/11
 */
public class UiManager : MonoBehaviour
{
    #region Fields

    [Header ("GameObject")]
    [SerializeField] private GameObject menuCam;
    [SerializeField] private GameObject gameCam;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header ("Text")]
    [SerializeField] Text maxScoreTxt;
    [SerializeField] Text scoreTxt_gamePanel;
    [SerializeField] Text scoreTxt_gameOverPanel;
    [SerializeField] Text stageTxt;
    [SerializeField] Text playTimeTxt;
    [SerializeField] Text playerHealthTxt;
    [SerializeField] Text playerAmmoTxt;
    [SerializeField] Text playerCoinTxt;
    [SerializeField] Text enemyATxt;
    [SerializeField] Text enemyBTxt;
    [SerializeField] Text enemyCTxt;

    [Header ("Image")]
    [SerializeField] Image weapon1Img;
    [SerializeField] Image weapon2Img;
    [SerializeField] Image weapon3Img;
    [SerializeField] Image weapon4Img;

    [Header ("RectTransform")]
    [SerializeField] RectTransform bossHealthGroup;
    [SerializeField] RectTransform bossHealrhBar;

    #endregion



    #region Property


    #endregion



    #region Funtion

    //--------------------------public--------------------------------------

    /**
     * @brief GameOver UI 설정 메서드
     * @details value값에 따라 다른 처리를 구현한 GameOver UI 설정 메서드입니다.
     * 
     * @param[in] value : 설정값
     * 
     * @author yws
     * @date last change 2022/08/14
     */
    public void SetGameOverUI(bool value)
    {
        gamePanel.SetActive(false);

        if(value == true)
            scoreTxt_gameOverPanel.text = scoreTxt_gamePanel.text;
        else
            menuPanel.SetActive(true);

        gameOverPanel.SetActive(value);
    }

    /**
     * @brief 게임 시작 시 Cam, Panel 설정 메서드
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public void SetupGameUI()
    {
        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    /**
     * @brief GameUI의 Boss UI 설정 메서드
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public void SetBossHealthBar(Boss boss)
    {
        if (boss != null)
            bossHealrhBar.localScale = new Vector3(boss.Health / boss.MaxHealth, 1, 1);
    }

    /**
     * @brief GameUI의 Score, 플레이 시간 UI 설정 메서드
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public void SetUtilityUI()
    {
        scoreTxt_gamePanel.text = string.Format("{0:n0}", GameManager.Instance.Score);
        stageTxt.text = $"Stage {GameManager.Instance.Stage}";

        TimeSpan time = TimeSpan.FromSeconds(GameManager.Instance.PlayTime);
        playTimeTxt.text = time.ToString(@"hh\:mm\:ss");
    }

    /**
     * @brief GameUI의 Player UI 설정 메서드
     * 
     * @param[in] player : Player
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public void SetPlayerUI(Player player)
    {
        playerHealthTxt.text = $"{player.Health} / {player.MaxHealth}";
        playerCoinTxt.text = string.Format("{0:n0}", player.Coin);

        if (player.GetCurrentWeaponType == Weapon.WeaponType.Hammer)
            playerAmmoTxt.text = $"- / 999";
        else
            playerAmmoTxt.text = $"{player.CurrentWeaponAmmo} / {player.CurrentWeaponMaxAmmo}";

        weapon1Img.color = new Color(1, 1, 1, player.HasWeapon[0] ? 1 : 0);
        weapon2Img.color = new Color(1, 1, 1, player.HasWeapon[1] ? 1 : 0);
        weapon3Img.color = new Color(1, 1, 1, player.HasWeapon[2] ? 1 : 0);
        weapon4Img.color = new Color(1, 1, 1, player.Grenades > 0 ? 1 : 0);
    }

    /**
     * @brief GameUI의 남은 적 UI 설정 메서드
     * 
     * @param[in] enemyACnt : enemy A의 남은 수
     * @param[in] enemyBCnt : enemy B의 남은 수
     * @param[in] enemyCCnt : enemy C의 남은 수
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public void SetEnemyUI(int enemyACnt, int enemyBCnt, int enemyCCnt)
    {
        enemyATxt.text = enemyACnt.ToString();
        enemyBTxt.text = enemyBCnt.ToString();
        enemyCTxt.text = enemyCCnt.ToString();
    }

    /**
     * @brief BossHealthGroup on/off 메서드
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    public void SetBossUI(bool value)
    {
        bossHealthGroup.gameObject.SetActive(value);
    }


    //--------------------------private--------------------------------------

    /**
     * @brief 메뉴 화면의 최고 점수 Text를 설정하는 메서드
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    private void SetupMaxScoreTxt()
    {
        try
        {
            maxScoreTxt.text = string.Format("{0:n0}", PlayerPrefs.GetInt("MaxScore"));
        }
        catch (PlayerPrefsException e)
        {
            maxScoreTxt.text = "0";
        }
    
    }


    #endregion



    #region Unity Event

    private void Awake()
    {
        SetupMaxScoreTxt();
    }

    #endregion


}
