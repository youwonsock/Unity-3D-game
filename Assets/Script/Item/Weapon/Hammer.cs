using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class WeaponŬ������ ����� ���� Hammer Ŭ����
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/13
 */
public class Hammer : Weapon
{

    #region Fields

    [Header("Hammer")]
    [SerializeField] private TrailRenderer trailRenderer;
    private BoxCollider attackArea;


    #endregion



    #region Property

    #endregion



    #region Funtion

    //--------------------------public-------------------------------------
    /**
     * @brief Hammer ���� �޼���
     * @details Hammer.cs���� �������� Weapon�� Attack()�Դϴ�
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    public override float Attack()
    {
        fireReady = false;
        StartCoroutine(Swing());

        return rate;
    }

    //--------------------------private-------------------------------------

    /**
     * @brief Hammer�� ������ �����̸� �����ϱ� ���� �ڷ�ƾ
     * @details Hammer�� fireReady�� attackArea, trailRenderer���� ����ȿ���� �ð��� �����ϱ� ���� �ڷ�ƾ�Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    IEnumerator Swing()
    {
        var wfs = new WaitForSecondsRealtime(rate - 0.1f);

        yield return new WaitForSecondsRealtime(0.1f);
        attackArea.enabled = true;
        trailRenderer.enabled = true;

        yield return wfs;
        attackArea.enabled = false;

        yield return wfs;
        trailRenderer.enabled = false;
        fireReady = true;

        yield break;
    }

    #endregion



    #region Unity Event

    private void Awake()
    {
        TryGetComponent<BoxCollider>(out attackArea);

        if (!attackArea || !trailRenderer)
            Debug.Log($"Some Component is null : {this.gameObject} .Hammer.cs");
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        fireReady = true;
    }

    /**
     * @brief OnTriggerStay() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerStay()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
     * 
     * @author yws
     * @data last change 2022/07/24
     */
    protected override void ActWhenTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        IDamageAble damageAble;
        other.gameObject.TryGetComponent<IDamageAble>(out damageAble);

        if (damageAble != null)
            damageAble.Hit(damage, transform.forward, 8);
    }

    #endregion

}
