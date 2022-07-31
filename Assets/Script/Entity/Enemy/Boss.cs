using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Enemy를 상속받은 Boss클래스
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
     * @brief boss.cs의 UpdateManager.SubscribeToFixedUpdate 구독 메서드
     * @details Update에서 실행해야하는 작업을 구현해두는 메서드입니다.
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
     * @brief boss의 공격 Coroutine
     * @details boss의 공격 Coroutine은 Random.Range를 사용해 정해진 패턴 중 1가지를 골라 공격합니다.
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
                //미사일
                StartCoroutine(LaunchMissile());
                break;

            case 2:
            case 3:
                //바위
                StartCoroutine(RockShot());
                break;

            case 4:
                //점프 공격
                StartCoroutine(Taunt());
                break;
        }

        yield break;
    }

    /**
     * @brief 점프 공격 Coroutine
     * @details 플레이어 추적은 nav를 이용
     * 
     * @author yws
     * @date last change 2022/07/30
     */
    IEnumerator Taunt()
    {
        tauntVec = target.position;

        // 플레이어 보기 중지 및 점프 시 충돌 방지를 위한 Layer변경
        isLookPlayer = false;
        nav.isStopped = false;
        this.gameObject.layer = 12;
        animator.SetTrigger("doTaunt");

        yield return new WaitForSecondsRealtime(1.5f);
        jumpAttackArea.enabled = true;
        
        yield return new WaitForSecondsRealtime(0.5f);
        jumpAttackArea.enabled = false;

        yield return new WaitForSecondsRealtime(1f);
        // 플레이어 보기 재시작 및 충돌 재활성화를 위한 Layer변경
        isLookPlayer = true;
        nav.isStopped = true;
        this.gameObject.layer = 9;

        StartCoroutine(BossPattern());
        yield break;
    }

    /**
     * @brief 바위 공격 Coroutine
     * @details RockShot을 사용하는 중에는 플레이어를 바라보는 행위를 잠시 멈춤니다.
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
     * @brief 미사일 공격 Coroutine
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
