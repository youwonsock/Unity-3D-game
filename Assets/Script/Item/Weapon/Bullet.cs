using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class 총알의 기능을 구현한 클레스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/15
 */
public class Bullet : MonoBehaviour
{
    [SerializeField] BulletData bulletData;
    private Rigidbody rigid;

    /**
     * @brief 총알을 이동시키는 메서드
     * @details 매개변수 firePos를 이용하여 총알의 위치를 초기화시킨후 전방으로 진행시킵니다.
     * 
     * @param[in] firePos : 발사위치
     * 
     * @author yws
     * @date last change 2022/07/17
     */
    public void SetBullet(Transform firePos)
    {
        transform.position = firePos.position;
        rigid.velocity = firePos.right * bulletData.Speed;
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

    private void Awake()
    {
        TryGetComponent<Rigidbody>(out rigid);

        if (!rigid)
            Debug.Log("bullet rigid is null");
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageAble damageAble;
        other.gameObject.TryGetComponent<IDamageAble>(out damageAble);

        if (damageAble != null)
        {
            damageAble.Hit(bulletData.Damage, transform.forward);
            ObjectPool.ReturnObject(this, bulletData.BulletType);
            return;
        }

        ObjectPool.ReturnObject(this, bulletData.BulletType);
    }
}
