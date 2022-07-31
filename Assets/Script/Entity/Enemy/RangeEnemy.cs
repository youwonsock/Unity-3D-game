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

    #endregion



    #region Funtions

    /**
     * @brief RangeEnemy�� ���Ÿ����� Coroutine
     * 
     * @author yws
     * @date last change 2022/07/25
     */
    IEnumerator RangeAttack()
    {
        //���� ����
        isChase = false;
        canAttack = false;

        rigid.velocity = Vector3.zero;
        animator.SetBool("isWalk", false);
        yield return new WaitForSecondsRealtime(1f);

        //���� ����
        animator.SetBool("isAttack", true);
        yield return new WaitForSecondsRealtime(0.7f);// �߻� �� ��� ����
        ObjectPool.GetObject(ObjectPool.BulletType.Missile).SetBullet(transform);
        animator.SetBool("isAttack", false);
        
        //���� ���� �� ���� ����
        isChase = true;
        animator.SetBool("isWalk", true);

        yield return new WaitForSecondsRealtime(attackCooltime);
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
