using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class �߻� Ŭ������ ItemŬ������ ����� ���� Weapon Ŭ����
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
     * @brief OnTriggerStay() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerStay()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
     * isPlayerWeapon�� True�ϰ�� Player�� ����ִ� �����̴� GetWeapon�� ȣ������ �ʽ��ϴ�.
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
     * @brief OnTriggerStay() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerExit()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
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
     * @brief �� ������� ���� �޼���
     * @details �ڽ� Ŭ�������� �������Ͽ� �� ������ ������ �����ϴ� �޼����Դϴ�.\n
     * ��ȯ���� float�� �̿��Ͽ� playerWeapon�� castingTime�� �̿��� player���� �̵��� ���� ���ϴ� �ð��� �����մϴ�.
     * 
     * @return float
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    public virtual float Attack() { return 0; }

    /**
     * @brief �� ������� ������ �޼���
     * @details �ڽ� Ŭ�������� �������Ͽ� �� ������ ������ �����ϴ� �޼����Դϴ�.\n
     * ��ȯ���� bool �̿��Ͽ� player���� ������ �ִϸ��̼��� ������� ���� �����մϴ�.\n
     * �������� �ʿ���� ��� override���� �ʽ��ϴ�.
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
