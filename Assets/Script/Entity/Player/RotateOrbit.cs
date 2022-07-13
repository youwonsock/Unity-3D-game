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

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief Player�� ���� ������ Grenade������ŭ List�� �Ҵ�� Grenade�� Ȱ��ȭ ��Ű�� �޼���
     * @details Player�� GetItem �̺�Ʈ�� ����Ͽ� ����մϴ�.
     * 
     * @author yws
     * @data last change 2022/07/10
     */
    private void ActiveOrbitGrenade()
    {
        for(int i = 0; i < player.Grenades; i++)
            list[i].SetActive(true);
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
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromFixedUpdate(RotateOrbitGrenades);
        player.ChangeGrenadesCount -= ActiveOrbitGrenade;
    }

    #endregion

}
