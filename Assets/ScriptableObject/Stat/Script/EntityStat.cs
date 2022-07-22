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
[CreateAssetMenu(fileName = "EntityStat", menuName = "Scriptable Object/EntityStat")]
public class EntityStat : ScriptableObject
{
    [Header ("Stat")]
    public float MaxHealth;

    [Header("Velocity and Force")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    [Header("Capacity")]
    [SerializeField] private int maxJumpCount;
    [SerializeField] private float dodgeTime;
    [SerializeField] private int dodgeCoolTime;

    #region Property

    public float WalkSpeed { get { return walkSpeed; } }
    public float RunSpeed { get { return runSpeed; } }
    public float JumpForce { get { return jumpForce; } }
    public int MaxJumpCount { get { return maxJumpCount; } }
    public float DodgeTime { get { return dodgeTime; } }
    public int DodgeCoolTime { get { return dodgeCoolTime; } }

    #endregion
}

