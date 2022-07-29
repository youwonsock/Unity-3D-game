using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class �Ѿ��� ����� ������ Ŭ����
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
     * @brief �Ѿ��� �̵���Ű�� �޼���
     * @details �Ű����� firePos�� �̿��Ͽ� �Ѿ��� ��ġ�� �ʱ�ȭ��Ų�� �������� �����ŵ�ϴ�.
     * 
     * @param[in] firePos : �߻���ġ
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

    /**
     * @brief ���� �ð� ����� ���� Coroutine
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
     * @brief ���� �� �߻� �Ѿ��� ���� Coroutine
     * @details ������ �ð��� ���� �� �Ѿ��� �߻��մϴ�.
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
