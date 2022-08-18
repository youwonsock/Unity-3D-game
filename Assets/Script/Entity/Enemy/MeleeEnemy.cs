using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Enemy를 상속받은 근접 공격 몬스터 클래스
 * @details 현재 coroutine에 사용되는 상수들을 stat으로 변경 예정
 * 
 * @author yws
 * @date last change 2022/07/24
 */
public class MeleeEnemy : Enemy
{
    #region Fields

    [SerializeField] private BoxCollider attackArea;

    #endregion



    #region Funtions

    /**
     * @brief MeleeEnemy의 근접공격 Coroutine
     * 
     * @author yws
     * @date last change 2022/07/23
     */
    IEnumerator MeleeAttack()
    {
        isChase = false;
        canAttack = false;
        animator.SetBool("isAttack", true);

        yield return new WaitForSecondsRealtime(0.3f);
        attackArea.enabled = true;

        yield return new WaitForSecondsRealtime(1f);
        attackArea.enabled = false;
        isChase = true;
        animator.SetBool("isAttack", false);

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
        StartCoroutine(MeleeAttack());
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

    #endregion
}
