using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class �߻� Ŭ������ ItemŬ������ ����� ���� Weapon Ŭ����
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/07
 */
public class Weapon : Item
{
    #region Fields

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief OnTriggerStay() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerStay()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected override void ActWhenTriggerStay()
    {

    }

    /**
     * @brief OnTriggerStay() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerExit()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected override void ActWhenTriggerExit()
    {

    }
    #endregion



    #region Unity Event

    #endregion
}
