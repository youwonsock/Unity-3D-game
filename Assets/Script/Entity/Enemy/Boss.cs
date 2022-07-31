using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Enemy�� ��ӹ��� BossŬ����
 * 
 * @author yws
 * @date last change 2022/07/30
 */
public class Boss : Enemy
{
    #region Fields

    [Header("Boss")]
    [SerializeField] Transform missilePortA;
    [SerializeField] Transform missilePortB;
    [SerializeField] BoxCollider jumpAttackArea;

    [SerializeField] bool isLookPlayer;
    private Vector3 tauntVec;

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief boss.cs�� UpdateManager.SubscribeToFixedUpdate ���� �޼���
     * @details Update���� �����ؾ��ϴ� �۾��� �����صδ� �޼����Դϴ�.
     * 
     * @author yws
     * @date last change 2022/07/25
     */
    protected override void OnUpdateWork()
    {
        if(nav.enabled)
            nav.SetDestination(tauntVec);

        if (!isDead && isLookPlayer)
        {
            transform.LookAt(target.position);
        }
    }

    /**
     * @brief boss�� ���� Coroutine
     * @details boss�� ���� Coroutine�� Random.Range�� ����� ������ ���� �� 1������ ��� �����մϴ�.
     * 
     * @author yws
     * @date last change 2022/07/30
     */
    IEnumerator BossPattern()
    {
        yield return new WaitForSecondsRealtime(attackCooltime);

        switch(Random.Range(0,5))
        {
            case 0:
            case 1:
                //�̻���
                StartCoroutine(LaunchMissile());
                break;

            case 2:
            case 3:
                //����
                StartCoroutine(RockShot());
                break;

            case 4:
                //���� ����
                StartCoroutine(Taunt());
                break;
        }

        yield break;
    }

    /**
     * @brief ���� ���� Coroutine
     * @details �÷��̾� ������ nav�� �̿�
     * 
     * @author yws
     * @date last change 2022/07/30
     */
    IEnumerator Taunt()
    {
        tauntVec = target.position;

        // �÷��̾� ���� ���� �� ���� �� �浹 ������ ���� Layer����
        isLookPlayer = false;
        nav.isStopped = false;
        this.gameObject.layer = 12;
        animator.SetTrigger("doTaunt");

        yield return new WaitForSecondsRealtime(1.5f);
        jumpAttackArea.enabled = true;
        
        yield return new WaitForSecondsRealtime(0.5f);
        jumpAttackArea.enabled = false;

        yield return new WaitForSecondsRealtime(1f);
        // �÷��̾� ���� ����� �� �浹 ��Ȱ��ȭ�� ���� Layer����
        isLookPlayer = true;
        nav.isStopped = true;
        this.gameObject.layer = 9;

        StartCoroutine(BossPattern());
        yield break;
    }

    /**
     * @brief ���� ���� Coroutine
     * @details RockShot�� ����ϴ� �߿��� �÷��̾ �ٶ󺸴� ������ ��� ����ϴ�.
     * 
     * @author yws
     * @date last change 2022/07/30
     */
    IEnumerator RockShot()
    {
        isLookPlayer = false;
        animator.SetTrigger("doBigShot");
        ObjectPool.GetObject(ObjectPool.BulletType.BossRock).SetBullet(transform);
        yield return new WaitForSecondsRealtime(3f);

        isLookPlayer = true;
        StartCoroutine(BossPattern());
        yield break;
    }

    /**
     * @brief �̻��� ���� Coroutine
     * 
     * @author yws
     * @date last change 2022/07/30
     */
    IEnumerator LaunchMissile()
    {
        var wfs = new WaitForSecondsRealtime(0.2f);

        animator.SetTrigger("doShot");
        yield return wfs;
        ObjectPool.GetObject(ObjectPool.BulletType.BossMissile).SetBullet(missilePortA);
        yield return wfs;
        ObjectPool.GetObject(ObjectPool.BulletType.BossMissile).SetBullet(missilePortB);

        yield return new WaitForSecondsRealtime(2.5f);

        StartCoroutine(BossPattern());
        yield break;
    }

    #endregion



    #region Unity Event

    protected override void Awake()
    {
        base.Awake();
        nav.isStopped = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        UpdateManager.SubscribeToUpdate(OnUpdateWork);

        StartCoroutine(BossPattern());
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        UpdateManager.UnsubscribeFromUpdate(OnUpdateWork);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IDamageAble damageAble;
            other.gameObject.TryGetComponent<IDamageAble>(out damageAble);

            if (damageAble != null)
                damageAble.Hit(damage, target.position - transform.position + Vector3.up * 10, 1f);
        }
    }

    #endregion
}
