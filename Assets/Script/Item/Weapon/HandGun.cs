using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class Weapon클래스의 상속을 받은 HandGun 클래스
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
     * @brief HandGun의 공격 메서드
     * @details Weapon의 Attack를 override하여 공격을 구현한 메서드 입니다.
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
     * @brief 발사 속도 구현을 위한 코루틴
     * @details rate만큼의 시간이 지난 후 fireReady를 초기화하여 발사 속도를 조절합니다.
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
