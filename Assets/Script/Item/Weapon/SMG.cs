using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class WeaponŬ������ ����� ���� SMG Ŭ����
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/07
 */
public class SMG : Weapon
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

    /**
     * @brief MagSize getter
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    public int MagSize { get { return magSize; } }

    /**
     * @brief currentMag Property
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    public int CurrentMag 
    { 
        get { return currentMag; } 
        set { currentMag = Mathf.Clamp(value, 0, magSize); }
    }


    #endregion



    #region Funtion

    //--------------------------public-------------------------------------

    /**
     * @brief SMG ���� �޼���
     * @details Weapon�� Attack�� override�Ͽ� ������ ������ �޼��� �Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/17
     */
    public override float Attack()
    {
        if(currentMag == 0)
        {
            fireReady = false;
            return -1;
        }

        fireReady = false;

        ObjectPool.GetObject(ObjectPool.BulletType.SMG).SetBullet(firePos);
        StartCoroutine(FireRate());

        return rate;
    }

    //--------------------------private-------------------------------------

    #endregion


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

    #region Unity Event

    private void OnEnable()
    {
        fireReady = true;

        if (bulletData != null)
        {
            bulletData.Damage = damage;
            bulletData.Speed = bulletSpeed;
            bulletData.BulletType = ObjectPool.BulletType.SMG;
        }
    }

    #endregion

}
