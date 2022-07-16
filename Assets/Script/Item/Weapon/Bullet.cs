using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class �Ѿ��� ����� ������ Ŭ����
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/15
 */

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletData bulletData;

    private void OnCollisionEnter(Collision collision)
    {

        IDamageAble damageAble;
        collision.gameObject.TryGetComponent<IDamageAble>(out damageAble);

        if (damageAble != null)
        {
            damageAble.Hit(bulletData.Damage, transform.position);
            ObjectPool.ReturnObject(this, bulletData.BulletType);
        }

        ObjectPool.ReturnObject(this, bulletData.BulletType);
    }
}
