using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class WeaponŬ������ ����� ���� Grenade Ŭ����
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/09
 */
public class Grenade : Item
{

    #region Fields

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
        if(other.CompareTag("Player"))
        { 
            other.GetComponent<Player>().Grenades = 1;

            Destroy(this.gameObject);
        }
    }

    #endregion



    #region Unity Event

    #endregion

}
