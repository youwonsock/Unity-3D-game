using UnityEngine;


/**
 * @brief class Entity Data를 저장하는 ScriptableObject의 base class 
 * @details 모든 Entity들이 가지고 있는 공통 Stat을 저장하는 ScriptableObject class입니다.\n
 * 각 Entity에게 필요한 ScriptableObject을 만들때 상속하여 사용합니다.
 * 
 * 
 * @author yws
 * @date last change 2022/07/10
 */
public class EntityStat : ScriptableObject
{
    [Header ("Stat")]
    public float MaxHealth;

    [Header("Velocity and Force")]
    [SerializeField] public float walkSpeed;
    [SerializeField] public float runSpeed;
    [SerializeField] public float jumpForce;

    [Header("Capacity")]
    [SerializeField] public int maxJumpCount;
    [SerializeField] public float dodgeTime;
    [SerializeField] public int dodgeCoolTime;
}

