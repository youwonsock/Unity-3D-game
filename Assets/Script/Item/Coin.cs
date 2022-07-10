using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class �߻� Ŭ������ ItemŬ������ ����� ���� Coin Ŭ����
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/09
 */
public class Coin : Item
{

    #region Fields

    //ScriptableObject
    [SerializeField] private ItemData itemData;

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief OnTriggerEnter() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerEnter()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/09
     */
    protected override void ActWhenTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Coin = itemData.amount;

            Destroy(this.gameObject);
        }
    }
    #endregion



    #region Unity Event

    #endregion

}
