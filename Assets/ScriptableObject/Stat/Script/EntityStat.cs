using UnityEngine;


/**
 * @brief class Entity Data�� �����ϴ� ScriptableObject�� base class 
 * @details ��� Entity���� ������ �ִ� ���� Stat�� �����ϴ� ScriptableObject class�Դϴ�.\n
 * �� Entity���� �ʿ��� ScriptableObject�� ���鶧 ����Ͽ� ����մϴ�.
 * 
 * 
 * @author yws
 * @date last change 2022/07/10
 */
public class EntityStat : ScriptableObject
{
    [Header ("Stat")]
    public float MaxHealth;
}

