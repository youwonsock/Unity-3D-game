using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @brief Class Player�� �ʻ���� Grenade�� ȸ����Ű�� Ŭ����
 * 
 * @author yws
 * @date last change 2022/07/10
 */
public class RotateOrbit : MonoBehaviour
{

    #region Fields

    [SerializeField] private Player player;
    [SerializeField] private int speed;
    [SerializeField] private List<GameObject> list;
    [SerializeField] private GameObject throwGrenadeObj;

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief Player�� ���� ������ Grenade������ŭ List�� �Ҵ�� Grenade�� Ȱ��ȭ ��Ű�� �޼���
     * @details Player�� ChangeGrenadesCount �̺�Ʈ�� ����Ͽ� ����մϴ�.
     * 
     * @author yws
     * @data last change 2022/07/10
     */
    private void ActiveOrbitGrenade()
    {
        int i = 0;
        while (i < list.Count)
        {
            if(i < player.Grenades)
                list[i].SetActive(true);
            else
                list[i].SetActive(false);

            i++;
        }

    }

    /**
     * @brief Player�� �ʻ���� Grenade�� ����ϴ� �޼���
     * @details Player�� UseGrenade �̺�Ʈ�� ����Ͽ� player�� grenadesInput�� true�� ��� ����˴ϴ�.
     * 
     * @param[in] throwVec : ������ ����
     * 
     * @author yws
     * @data last change 2022/07/23
     */
    private void ThrowGrenade(Vector3 throwVec)
    {
        if (player.Grenades > 0)
        {
            player.Grenades--;
            Grenade throwGrenade;
            GameObject instantGrenade = Instantiate(throwGrenadeObj, transform.position, transform.rotation);
            instantGrenade.TryGetComponent<Grenade>(out throwGrenade);
            throwGrenade.Throw(throwVec);
        }
    }

    /**
     * @brief Grenade Group�� ȸ�������ִ� �޼���
     * 
     * @author yws
     * @data last change 2022/07/10
     */
    private void RotateOrbitGrenades()
    {
        transform.Rotate(Vector3.up * speed);
    }

    #endregion



    #region Unity Event

    private void OnEnable()
    {
        UpdateManager.SubscribeToFixedUpdate(RotateOrbitGrenades);
        player.ChangeGrenadesCount += ActiveOrbitGrenade;
        player.UseGrenade += ThrowGrenade;
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromFixedUpdate(RotateOrbitGrenades);
        player.ChangeGrenadesCount -= ActiveOrbitGrenade;
        player.UseGrenade -= ThrowGrenade;
    }

    #endregion

}
