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
    #region Fields

    [SerializeField] BulletData bulletData;
    private Rigidbody rigid;

    #endregion



    #region Funtion

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
        transform.rotation = firePos.rotation;
        rigid.velocity = firePos.forward * bulletData.Speed;
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

    #endregion



    #region Unity Event

    protected virtual void Awake()
    {
        TryGetComponent<Rigidbody>(out rigid);

        if (!rigid)
            Debug.Log("bullet rigid is null");
    }

    private void OnTriggerEnter(Collider other)
    {
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
