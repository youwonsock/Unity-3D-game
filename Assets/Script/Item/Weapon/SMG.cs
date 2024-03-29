using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class Weapon클래스의 상속을 받은 SMG 클래스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/22
 */
public class SMG : Weapon
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
     * @brief SMG 공격 메서드
     * @details Weapon의 Attack를 override하여 공격을 구현한 메서드 입니다.\n
     * 첫 총알 발사시 바닥에 박히는 현상
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    public override float Attack()
    {
        if(currentMag == 0 || isReload)
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
     * @brief SMG의 재장전 메서드
     * @details Weapon의 Reload를 override하여 재장전을 구현한 메서드 입니다.\n
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

    #endregion

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
     * @brief 재장전시 발사 방지를 위한 코루틴
     * @details rate만큼의 시간이 지난 후 fireReady를 초기화하여 발사 속도를 조절합니다.
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
     * @brief 초탄이 바닥에 박히는 현상을 수정하기 위해 추가된 Invoke 실행용 메서드
     * @details Invoke를 이용해 지연 호출하여 총알이 바닥에 박히는 현상을 방지해줍니다.
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    private void Fire()
    {
        ObjectPool.GetObject(ObjectPool.BulletType.SMG).SetBullet(firePos);
    }

    #region Unity Event

    protected override void OnEnable()
    {
        base.OnEnable();

        if (bulletData != null)
        {
            bulletData.Damage = damage;
            bulletData.Speed = bulletSpeed;
            bulletData.BulletType = ObjectPool.BulletType.SMG;
        }
    }

    #endregion

}
