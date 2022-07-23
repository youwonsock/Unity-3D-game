using System.Collections;
using UnityEngine;

/**
 * @brief DamageSystem�� �ܼ�ȭ�� ���� ������ interface
 * @details IDamageAble�� ��ӹ��� Ŭ�������� �ǰݽ� ó���� �����Ͽ� \n
 * �����ϴ� ��ü�� IDamageAble�� ������ �ִ����� �˰� ������ ������ �����մϴ�.
 * 
 * @author yws
 * @date last change 2022/07/01
 */
public interface IDamageAble
{
    /**
     * @brief DamageSystem�� �ܼ�ȭ�� ���� ������ interface
     * @details IDamageAble�� ��ӹ��� Ŭ�������� �ǰݽ� ó���� �����Ͽ� \n
     * �����ϴ� ��ü�� IDamageAble�� ������ �ִ����� �˰� ������ ������ �����մϴ�.
     * 
     * @param[in] damage : ���ط�
     * @param[in] knockbackForce : �˹��Ű�� ��
     * @param[in] direction : �ǰݹ���
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    public bool Hit(float damage, Vector3 direction, float knockbackForce = 0);
}
