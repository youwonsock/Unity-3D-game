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
        currentMag--;
        Invoke(nameof(Fire), 0.1f);
        StartCoroutine(FireRate());

        return rate;
    }

    /**
     * @brief HandGun의 재장전 메서드
     * @details Weapon의 Reload를 override하여 재장전을 구현한 메서드 입니다.
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

    /**
     * @brief 초탄이 바닥에 박히는 현상을 수정하기 위해 추가된 Invoke 실행용 메서드
     * @details Invoke를 이용해 지연 호출하여 총알이 바닥에 박히는 현상을 방지해줍니다.
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
