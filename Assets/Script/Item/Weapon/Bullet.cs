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
    private Rigidbody rigid;

    /**
     * @brief �Ѿ��� �̵���Ű�� �޼���
     * @details �Ű����� firePos�� �̿��Ͽ� �Ѿ��� ��ġ�� �ʱ�ȭ��Ų�� �������� �����ŵ�ϴ�.
     * 
     * @param[in] firePos : �߻���ġ
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
     * @brief �Ѿ��� ��ȯ�ϴ� �޼��� Invoke �̿�
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
