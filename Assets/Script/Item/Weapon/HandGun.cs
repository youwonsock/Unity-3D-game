using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class WeaponŬ������ ����� ���� HandGun Ŭ����
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/22
 */
public class HandGun : Weapon
{

    #region Fields
    [SerializeField] int magSize;
    [SerializeField] float reloadTime;

    [SerializeField] float bulletSpeed;
    [SerializeField] Transform firePos;
    [SerializeField] BulletData bulletData;

    private bool isReload = false;

    #endregion



    #region Property

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
        if (currentMag == 0 || isReload)
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
     * @details Weapon�� Reload�� override�Ͽ� �������� ������ �޼��� �Դϴ�.\n
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    public override bool Reload()
    {
        if (currentMag == magSize || Ammo == 0 || (!fireReady && currentMag != 0))
            return false;

        fireReady = false;
        StartCoroutine(ReloadRate());

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
     * @brief �������� �߻� ������ ���� �ڷ�ƾ
     * @details rate��ŭ�� �ð��� ���� �� fireReady�� �ʱ�ȭ�Ͽ� �߻� �ӵ��� �����մϴ�.
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    IEnumerator ReloadRate()
    {
        yield return new WaitForSecondsRealtime(reloadTime);

        if (Ammo - (magSize - currentMag) < 0)
        {
            currentMag += Ammo;
            Ammo = 0;
        }
        else
        {
            Ammo -= (magSize - currentMag);
            currentMag = magSize;
        }

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

    protected override void OnEnable()
    {
        base.OnEnable();

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
