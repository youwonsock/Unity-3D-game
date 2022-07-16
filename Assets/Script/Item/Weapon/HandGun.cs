using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class WeaponŬ������ ����� ���� HandGun Ŭ����
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/07
 */
public class HandGun : Weapon
{

    #region Fields
    [SerializeField] int magSize;
    [SerializeField] int currentMag;

    [SerializeField] float bulletSpeed;
    [SerializeField] Transform firePos;
    [SerializeField] BulletData bulletData;

    #endregion



    #region Property

    public int CurrentMag
    {
        get { return currentMag; }
        set { currentMag = value; }
    }

    #endregion



    #region Funtion

    //--------------------------public-------------------------------------
    
    /**
     * @brief HandGun�� ���� �޼���
     * @details Weapon�� Attack�� override�Ͽ� ������ ������ �޼��� �Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    public override float Attack()
    {
        if (currentMag == 0)
        {
            fireReady = false;
            return -1;
        }

        fireReady = false;
        currentMag--;
        Invoke(nameof(Fire), 0.1f);
        StartCoroutine(FireRate());

        return rate;
    }

    /**
     * @brief HandGun�� ������ �޼���
     * @details Weapon�� Reload�� override�Ͽ� �������� ������ �޼��� �Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    public override bool Reload()
    {
        if(currentMag == magSize || Ammo == 0)
            return false;

        if (Ammo - (magSize - currentMag) < 0)
        {
            currentMag += Ammo;
            Ammo = 0;
        }
        else
        {
            Ammo -= (magSize-currentMag);
            currentMag = magSize;
        }
        fireReady = true;

        return true;
    }

    //--------------------------private-------------------------------------

    /**
     * @brief �߻� �ӵ� ������ ���� �ڷ�ƾ
     * @details rate��ŭ�� �ð��� ���� �� fireReady�� �ʱ�ȭ�Ͽ� �߻� �ӵ��� �����մϴ�.
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    IEnumerator FireRate()
    {
        yield return new WaitForSecondsRealtime(rate);
        fireReady = true;

        yield break;
    }

    /**
     * @brief ��ź�� �ٴڿ� ������ ������ �����ϱ� ���� �߰��� Invoke ����� �޼���
     * @details Invoke�� �̿��� ���� ȣ���Ͽ� �Ѿ��� �ٴڿ� ������ ������ �������ݴϴ�.
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    private void Fire()
    {
        ObjectPool.GetObject(ObjectPool.BulletType.HandGun).SetBullet(firePos);
    }

    #endregion



    #region Unity Event

    private void OnEnable()
    {
        fireReady = true;

        if (bulletData != null)
        {
            bulletData.Damage = damage;
            bulletData.Speed = bulletSpeed;
            bulletData.BulletType = ObjectPool.BulletType.HandGun;
        }
    }

    #endregion

}
