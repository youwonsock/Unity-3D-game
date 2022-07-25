using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Enemy�� ��ӹ��� ���Ÿ� ���� ���� Ŭ����
 * @details ���� coroutine�� ���Ǵ� ������� stat���� ���� ����
 * 
 * @author yws
 * @date last change 2022/07/25
 */
public class RangeEnemy : Enemy
{
    #region Fields

    [SerializeField] private float attackDistance;

    #endregion



    #region Funtions

    /**
     * @brief RangeEnemy�� ���Ÿ����� �ڷ�ƾ
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
        yield return new WaitForSecondsRealtime(0.7f);// �߻� �� ��� ����
        ObjectPool.GetObject(ObjectPool.BulletType.Missile).SetBullet(transform);
        animator.SetBool("isAttack", false);
        
        isChase = true;//�ٽ� ���� ����
        animator.SetBool("isWalk", true);

        yield return new WaitForSecondsRealtime(attackCooltime);
        canAttack = true;

        yield break;
    }

    /**
     * @brief RangeEnemy.cs�� UpdateManager.SubscribeToFixedUpdate ���� �޼���
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
            StartCoroutine(RangeAttack());
        }
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
