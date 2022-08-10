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
    [SerializeField] protected float damage;
    [SerializeField] protected float rate;
    [SerializeField] protected int currentMag;
    protected bool fireReady = true;

    #endregion



    #region Property

    /**
     * @brief currentMag Property
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public int CurrentMag
    {
        get
        {
            return currentMag;
        }
        set
        {
            currentMag = Mathf.Clamp(value, 0, currentMag);
        }
    }

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
            ammo = Mathf.Clamp(value, 0, maxAmmo);
        } 
    }

    /**
     * @brief maxAmmo getter
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public int MaxAmmo { get { return maxAmmo; } }

    /**
     * @brief FireReady getter
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public bool FireReady { get { return fireReady; } }

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
     * @details 자식 클래스에서 재정의하여 각 무기의 공격을 구현하는 메서드입니다.\n
     * 반환값인 float를 이용하여 playerWeapon의 castingTime을 이용해 player에서 이동을 하지 못하는 시간을 설정합니다.
     * 
     * @return float
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    public virtual float Attack() { return 0; }

    /**
     * @brief 각 무기들의 재장전 메서드
     * @details 자식 클래스에서 재정의하여 각 무기의 공격을 구현하는 메서드입니다.\n
     * 반환값인 bool 이용하여 player에서 재장전 애니메이션을 재생할지 말지 결정합니다.\n
     * 재장전이 필요없는 경우 override하지 않습니다.
     * 
     * @return float
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    public virtual bool Reload() { return false; }



    #endregion



    #region Unity Event



    #endregion
}
