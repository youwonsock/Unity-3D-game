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
    [SerializeField] Transform bulletCasePos;
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

        ObjectPool.GetObject(ObjectPool.BulletType.HandGun).SetBullet(firePos);
        StartCoroutine(FireRate());

        return rate;
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
