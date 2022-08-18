using UnityEngine;

/**
 * @brief class Player의 Data를 저장하는 ScriptableObject
 * 
 * @author yws
 * @date last change 2022/08/19
 */
[CreateAssetMenu(fileName = "PlayerStat", menuName = "Scriptable Object/PlayerStat")]
public class PlayerStat : EntityStat
{
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
