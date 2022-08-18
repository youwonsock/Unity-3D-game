using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Enemy를 상속받은 돌진 공격 몬스터 클래스
 * @details 현재 coroutine에 사용되는 상수들을 stat으로 변경 예정
 * 
 * @author yws
 * @date last change 2022/07/25
 */
public class RushEnemy : Enemy
{
    #region Fields

    [SerializeField] private BoxCollider attackArea;

    #endregion



    #region Funtions

    /**
     * @brief RushEnemy의 돌진공격 Coroutine
     * 
     * @author yws
     * @date last change 2022/07/25
     */
    IEnumerator RushAttack()
    {
        //추적 상태 종료
        isChase = false;
        canAttack = false;

        rigid.velocity = Vector3.zero;
        animator.SetBool("isWalk", false);
        yield return new WaitForSecondsRealtime(1f);

        //공격 시작
        animator.SetBool("isAttack", true);
        attackArea.enabled = true;
        rigid.AddForce(transform.forward * 20, ForceMode.Impulse);

        yield return new WaitForSecondsRealtime(1.5f);

        //공격 종료
        attackArea.enabled = false;
        rigid.velocity = Vector3.zero;
        animator.SetBool("isAttack", false);

        //다시 추적 상태로 복귀
        isChase = true;// 바로 다시 추적 시작
        animator.SetBool("isWalk", true);

        yield return new WaitForSecondsRealtime(enemyStat.AttackCooltime);
        canAttack = true;

        yield break;
    }

    /**@brief Enemy의 Attack를 override한 메서드
     * @details Enemy의 Attack를 자식클래스에서 override하여 각 Enemy의 공격을 구현합니다.
     * 
     * @author yws
     * @date last change 2022/08/01
     */
    protected override void Attack()
    {
        StartCoroutine(RushAttack());
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
                damageAble.Hit(enemyStat.Damage, transform.forward, 10);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnDeath += () => StopCoroutine(RushAttack());
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        OnDeath -= () => StopCoroutine(RushAttack());
    }

    #endregion
}
