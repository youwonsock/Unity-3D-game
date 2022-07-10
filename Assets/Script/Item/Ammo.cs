using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class �߻� Ŭ������ ItemŬ������ ����� ���� Ammo Ŭ����
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/09
 */
public class Ammo : Item
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
            other.GetComponent<PlayerWeapon>().FillAmmo(itemData.amount);

            Destroy(this.gameObject);
        }
    }
    #endregion



    #region Unity Event

    #endregion

}
