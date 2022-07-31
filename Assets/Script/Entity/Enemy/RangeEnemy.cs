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

    #endregion



    #region Funtions

    /**
     * @brief RangeEnemy의 원거리공격 Coroutine
     * 
     * @author yws
     * @date last change 2022/07/25
     */
    IEnumerator RangeAttack()
    {
        //추적 종료
        isChase = false;
        canAttack = false;

        rigid.velocity = Vector3.zero;
        animator.SetBool("isWalk", false);
        yield return new WaitForSecondsRealtime(1f);

        //공격 시작
        animator.SetBool("isAttack", true);
        yield return new WaitForSecondsRealtime(0.7f);// 발사 시 잠시 멈춤
        ObjectPool.GetObject(ObjectPool.BulletType.Missile).SetBullet(transform);
        animator.SetBool("isAttack", false);
        
        //공격 종료 및 추적 시작
        isChase = true;
        animator.SetBool("isWalk", true);

        yield return new WaitForSecondsRealtime(attackCooltime);
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
        StartCoroutine(RangeAttack());
    }
    #endregion



    #region Unity Event

    protected override void OnEnable()
    {
        base.OnEnable();
        OnDeath += () => StopCoroutine(RangeAttack());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnDeath -= () => StopCoroutine(RangeAttack());
    }

    #endregion

}
