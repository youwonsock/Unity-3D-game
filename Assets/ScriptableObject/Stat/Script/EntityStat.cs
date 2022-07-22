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

