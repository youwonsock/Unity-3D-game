using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Enemy�� ��ӹ��� ���� ���� ���� Ŭ����
 * @details ���� coroutine�� ���Ǵ� ������� stat���� ���� ����
 * 
 * @author yws
 * @date last change 2022/07/25
 */
public class RushEnemy : Enemy
{
    #region Fields

    [SerializeField] private BoxCollider attackArea;
    [SerializeField] private float attackDistance;

    #endregion



    #region Funtions

    /**
     * @brief RushEnemy�� �������� �ڷ�ƾ
     * 
     * @author yws
     * @date last change 2022/07/25
     */
    IEnumerator RushAttack()
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

        isChase = true;// �ٷ� �ٽ� ���� ����
        animator.SetBool("isWalk", true);

        yield return new WaitForSecondsRealtime(attackCooltime);
        canAttack = true;

        yield break;
    }

    /**
     * @brief RushEnemy.cs�� UpdateManager.SubscribeToFixedUpdate ���� �޼���
     * @details FixedUpdate���� �����ؾ��ϴ� �۾��� �����صδ� �޼����Դϴ�.
     * 
     * @author yws
     * @date last change 2022/07/25
     */
    protected override void OnFixedUpdateWork()
    {
        base.OnFixedUpdateWork();

        if (canAttack && targetDistance < attackDistance)
        {
            StartCoroutine(RushAttack());
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
