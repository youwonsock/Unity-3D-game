using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class 
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/09
 */
public abstract class Item : MonoBehaviour
{
    #region Fields
    //public 
    public enum ItemType { Ammo, Coin, Grenade, Heart, Weapon };

    //SerializeField
    [SerializeField]private ItemType type;
    [SerializeField]private int value;
    [SerializeField]private bool isRotate;

    #endregion



    #region Property

    public int Value { get { return value; } }

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
    protected virtual void ActWhenTriggerStay(Collider other) { }

    /**
     * @brief OnTriggerExit() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerExit()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected virtual void ActWhenTriggerExit(Collider other) { }

    /**
     * @brief OnTriggerEnter() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerEnter()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/09
     */
    protected virtual void ActWhenTriggerEnter(Collider other) { }

    #endregion



    #region Unity Event

    private void OnEnable()
    {
        if(isRotate)
            UpdateManager.SubscribeToFixedUpdate(RotateItem);
    }

    private void OnDisable()
    {
        if(isRotate)
            UpdateManager.UnsubscribeFromFixedUpdate(RotateItem);
    }

    private void OnTriggerEnter(Collider other)
    {
        ActWhenTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        ActWhenTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        ActWhenTriggerExit(other);
    }

    #endregion

}
