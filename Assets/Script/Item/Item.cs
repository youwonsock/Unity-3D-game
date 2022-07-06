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
     * @brief Subscibe(, Fixed, Late)Update <- 이벤트 등록 메서드들
     * @details 이벤트를 등록하는 클래스의 OnEnable에서 Subscribe~ 메서드들을 호출하여 사용합니다.
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
    protected abstract void ActWhenTriggerStay(Collider other);

    /**
     * @brief OnTriggerExit() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerExit()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
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
