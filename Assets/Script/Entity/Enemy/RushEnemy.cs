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

    #endregion



    #region Funtions

    /**
     * @brief RushEnemy�� �������� Coroutine
     * 
     * @author yws
     * @date last change 2022/07/25
     */
    IEnumerator RushAttack()
    {
        //���� ���� ����
        isChase = false;
        canAttack = false;

        rigid.velocity = Vector3.zero;
        animator.SetBool("isWalk", false);
        yield return new WaitForSecondsRealtime(1f);

        //���� ����
        animator.SetBool("isAttack", true);
        attackArea.enabled = true;
        rigid.AddForce(transform.forward * 20, ForceMode.Impulse);

        yield return new WaitForSecondsRealtime(1.5f);

        //���� ����
        attackArea.enabled = false;
        rigid.velocity = Vector3.zero;
        animator.SetBool("isAttack", false);

        //�ٽ� ���� ���·� ����
        isChase = true;// �ٷ� �ٽ� ���� ����
        animator.SetBool("isWalk", true);

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
