using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Enemy를 상속받은 원거리 공격 몬스터 클래스
 * @details 현재 coroutine에 사용되는 상수들을 stat으로 변경 예정
 * 
 * @author yws
 * @date last change 2022/07/25
 */
public class RangeEnemy : Enemy
{
    #region Fields

    [SerializeField] private Collider attackArea;
    [SerializeField] private float attackDistance;

    #endregion



    #region Funtions

    /**
     * @brief RangeEnemy의 원거리공격 코루틴
     * 
     * @author yws
     * @date last change 2022/07/25
     */
    IEnumerator RangeAttack()
    {
        isChase = false;
        canAttack = false;

        rigid.velocity = Vector3.zero;
        animator.SetBool("isWalk", false);
        yield return new WaitForSecondsRealtime(1f);
        animator.SetBool("isAttack", true);
        attackArea.enabled = true;
        rigid.AddForce(transform.forward * 20, ForceMode.Impulse);

        yield return new WaitForSecondsRealtime(1.5f);
        attackArea.enabled = false;
        rigid.velocity = Vector3.zero;
        animator.SetBool("isAttack", false);

        isChase = true;// 바로 다시 추적 시작
        animator.SetBool("isWalk", true);

        yield return new WaitForSecondsRealtime(attackCooltime);
        canAttack = true;

        yield break;
    }

    /**
     * @brief RangeEnemy.cs의 UpdateManager.SubscribeToFixedUpdate 구독 메서드
     * @details FixedUpdate에서 실행해야하는 작업을 구현해두는 메서드입니다.
     * 
     * @author yws
     * @date last change 2022/07/25
     */
    protected override void OnFixedUpdateWork()
    {
        base.OnFixedUpdateWork();

        if (canAttack && targetDistance < attackDistance)
        {
            StartCoroutine(RangeAttack());
        }
    }

    #endregion



    #region Unity Event

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IDamageAble damageAble;
            other.gameObject.TryGetComponent<IDamageAble>(out damageAble);

            if (damageAble != null)
                damageAble.Hit(damage, transform.forward, 10);
        }
    }

    #endregion

}
