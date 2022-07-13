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
     * @details �ڽ� Ŭ�������� �������Ͽ� �� ������ ������ �����ϴ� �޼����Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    public virtual void Attack() { }

    #endregion



    #region Unity Event



    #endregion
}
