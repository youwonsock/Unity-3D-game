using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class 
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/22
 */
public abstract class Item : MonoBehaviour
{
    #region Fields
    //public 
    public enum ItemType { Ammo, Coin, Grenade, Heart, Weapon };

    [Header ("Item")]
    //SerializeField
    [SerializeField]private ItemType type;
    [SerializeField]private int itemCode;   //아이템의 고유값 현재는 필요 X
    [SerializeField]private bool isRotate;

    #endregion



    #region Property

    public int ItemCode { get { return itemCode; } }

    #endregion


    #region Funtion


    /**
     * @brief 아이템 회전 메서드
     * @details 드랍 아이템을 회전시켜주는 메서드입니다.\n
     * isRotate가 True일 경우 UpdateManager에 등록되어 아이템을 회전시킵니다.
     * 
     * @author yws
     * @data last change 2022/06/26
     */
    private void RotateItem()
    {
        transform.Rotate(Vector3.up * 2);
    }

    /**
     * @brief OnTriggerStay() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerStay()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected virtual void ActWhenTriggerStay(Collider other) { }

    /**
     * @brief OnTriggerExit() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerExit()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected virtual void ActWhenTriggerExit(Collider other) { }

    /**
     * @brief OnTriggerEnter() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerEnter()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/09
     */
    protected virtual void ActWhenTriggerEnter(Collider other) { }

    #endregion



    #region Unity Event

    protected virtual void OnEnable()
    {
        if(isRotate)
            UpdateManager.SubscribeToFixedUpdate(RotateItem);
    }

    protected virtual void OnDisable()
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
