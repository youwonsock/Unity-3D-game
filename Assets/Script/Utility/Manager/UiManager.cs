using System;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief class UI ������ ���� Manager Ŭ����
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

    [Header ("Text")]
    [SerializeField] Text maxScoreTxt;
    [SerializeField] Text scoreTxt;
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
     * @brief ���� ���� �� Cam, Panel ���� �޼���
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
     * @brief GameUI�� Boss UI ���� �޼���
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public void SetBossUI(Boss boss)
    {
        bossHealrhBar.localScale = new Vector3(boss.Health / boss.MaxHealth, 1, 1);
    }

    /**
     * @brief GameUI�� Score, �÷��� �ð� UI ���� �޼���
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public void SetUtilityUI()
    {
        scoreTxt.text = string.Format("{0:n0}", GameManager.Instance.Score);
        stageTxt.text = $"Stage {GameManager.Instance.Stage}";

        TimeSpan time = TimeSpan.FromSeconds(GameManager.Instance.PlayTime);
        playTimeTxt.text = time.ToString(@"hh\:mm\:ss");
    }

    /**
     * @brief GameUI�� Player UI ���� �޼���
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
     * @brief GameUI�� ���� �� UI ���� �޼���
     * 
     * @param[in] enemyACnt : enemy A�� ���� ��
     * @param[in] enemyBCnt : enemy B�� ���� ��
     * @param[in] enemyCCnt : enemy C�� ���� ��
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

    //--------------------------private--------------------------------------

    /**
     * @brief �޴� ȭ���� �ְ� ���� Text�� �����ϴ� �޼���
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
