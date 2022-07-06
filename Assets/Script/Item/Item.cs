using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class 
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/03
 */
public abstract class Item : MonoBehaviour
{
    #region Fields
    //public 
    public enum ItemType { Ammo, Coin, Grenade, Heart, Weapon };

    //private
    [SerializeField]private ItemType type;
    [SerializeField]private int value;

    #endregion



    #region Property

    #endregion


    #region Funtion


    /**
     * @brief Subscibe(, Fixed, Late)Update <- �̺�Ʈ ��� �޼����
     * @details �̺�Ʈ�� ����ϴ� Ŭ������ OnEnable���� Subscribe~ �޼������ ȣ���Ͽ� ����մϴ�.
     * 
     * @author yws
     * @data last change 2022/06/26
     */
    private void RotateItem()
    {
        transform.Rotate(Vector3.up * 2);
    }

    /**
     * @brief OnTriggerStay() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerStay()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected abstract void ActWhenTriggerStay(Collider other);

    /**
     * @brief OnTriggerExit() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerExit()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected abstract void ActWhenTriggerExit(Collider other);

    #endregion



    #region Unity Event

    private void OnEnable()
    {
        UpdateManager.SubscribeToFixedUpdate(RotateItem);
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromFixedUpdate(RotateItem);
    }

    private void OnTriggerStay(Collider other)
    {
        ActWhenTriggerStay();
    }

    private void OnTriggerExit(Collider other)
    {
        ActWhenTriggerExit();
    }

    #endregion

}
