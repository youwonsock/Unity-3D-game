using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class 추상 클래스인 Item클래스의 상속을 받은 Weapon 클래스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/13
 */
public class Weapon : Item
{
    #region Fields

    public enum WeaponType {Hammer, HandGun, SMG};

    [Header ("Weapon")]
    public WeaponType weaponType;

    //SerializeField
    [SerializeField] private bool isPlayerWeapon;
    [SerializeField] private int ammo;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float damage;
    [SerializeField] private float rate;

    #endregion



    #region Property

    /**
     * @brief ammo Property
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public int Ammo 
    { 
        get 
        { 
            return ammo; 
        } 
        set 
        {
            if (ammo + value > maxAmmo)
                ammo = maxAmmo;
            else
                ammo += value;
        } 
    }

    /**
     * @brief maxAmmo getter
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public int MaxAmmo { get { return maxAmmo; } }

    #endregion



    #region Funtion

    /**
     * @brief OnTriggerStay() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerStay()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * isPlayerWeapon이 True일경우 Player가 들고있는 무기이니 GetWeapon을 호출하지 않습니다.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected override void ActWhenTriggerStay(Collider other)
    { 
        if(!isPlayerWeapon && other.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().GetWeapon(weaponType))
                Destroy(this.gameObject);
        }
    }

    /**
     * @brief OnTriggerStay() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerExit()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected override void ActWhenTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    /**
     * @brief 각 무기들의 공격 메서드
     * @details 자식 클래스에서 재정의하여 각 무기의 공격을 구현하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    public virtual void Attack() { }

    #endregion



    #region Unity Event



    #endregion
}
