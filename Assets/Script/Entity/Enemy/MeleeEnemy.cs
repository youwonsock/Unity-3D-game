using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Enemy�� ��ӹ��� ���� ���� ���� Ŭ����
 * @details ���� coroutine�� ���Ǵ� ������� stat���� ���� ����
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
     * @brief MeleeEnemy�� �������� Coroutine
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

    /**@brief Enemy�� Attack�� override�� �޼���
     * @details Enemy�� Attack�� �ڽ�Ŭ�������� override�Ͽ� �� Enemy�� ������ �����մϴ�.
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
