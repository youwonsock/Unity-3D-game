using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class 총알의 기능을 구현한 클레스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/28
 */
public class Bullet : MonoBehaviour
{
    #region Fields

    [SerializeField] BulletData bulletData;
    [SerializeField] bool isCharging;
    private Rigidbody rigid;
    private bool isShot;

    #endregion



    #region Funtion

    /**
     * @brief 총알을 이동시키는 메서드
     * @details 매개변수 firePos를 이용하여 총알의 위치를 초기화시킨후 전방으로 진행시킵니다.
     * 
     * @param[in] firePos : 발사위치
     * 
     * @author yws
     * @date last change 2022/07/28
     */
    public void SetBullet(Transform firePos)
    {
        transform.position = firePos.position;
        transform.rotation = firePos.rotation;

        if (isCharging)
        {
            StartCoroutine(GainPowerTimer());
            StartCoroutine(GainPower());
        }
        else
            rigid.velocity = firePos.forward * bulletData.Speed;

        Invoke(nameof(ReturnBullet), bulletData.Time);    
    }

    /**
     * @brief 총알을 반환하는 메서드 Invoke 이용
     * 
     * @author yws
     * @date last change 2022/07/17
     */
    private void ReturnBullet()
    {
        if(this.gameObject.activeSelf)
            ObjectPool.ReturnObject(this, bulletData.BulletType);
    }

    /**
     * @brief 충전 시간 계산을 위한 Coroutine
     * 
     * @author yws
     * @date last change 2022/07/28
     */
    IEnumerator GainPowerTimer()
    {
        float time = Random.Range(1.5f, 2.5f);

        yield return new WaitForSecondsRealtime(time);
        isShot = true;

        yield break;
    }

    /**
     * @brief 충전 후 발사 총알의 충전 Coroutine
     * @details 랜덤한 시간이 지난 후 총알을 발사합니다.
     * 
     * @author yws
     * @date last change 2022/07/28
     */
    IEnumerator GainPower()
    {
        float angularPower = 2;
        float scale = 0.1f;

        while (!isShot)
        {
            angularPower += 0.02f;
            scale += 0.002f;
            transform.localScale = Vector3.one * scale;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);
            yield return null;
        }

        isShot = false;
        yield break;
    }

    #endregion



    #region Unity Event

    protected virtual void Awake()
    {
        TryGetComponent<Rigidbody>(out rigid);

        if (!rigid)
            Debug.Log("bullet rigid is null");
    }

    private void OnEnable()
    {
        SetBullet(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCharging && other.CompareTag("Floor"))
            return;

        if (bulletData.Shooter == Constants.Shooter.Player && other.gameObject.CompareTag("Player"))
            return;
        else if (bulletData.Shooter == Constants.Shooter.Enemy && other.gameObject.CompareTag("Enemy"))
            return;

        IDamageAble damageAble;
        other.gameObject.TryGetComponent<IDamageAble>(out damageAble);

        if (damageAble != null)
        {
            damageAble.Hit(bulletData.Damage, transform.forward);
            ObjectPool.ReturnObject(this, bulletData.BulletType);
            return;
        }

        ObjectPool.ReturnObject(this, bulletData.BulletType);
        Debug.Log(this.gameObject + "bullet is end");
    }

    #endregion
}
