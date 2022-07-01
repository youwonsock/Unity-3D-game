using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Entity 객체들의 이동을 구현한 클레스
 * @details Entity 객체들의 실질적인 이동을 구현한 클래스입니다.\n
 * 
 * @author yws
 * @date last change 2022/06/28
 */
public class EntityMovement : MonoBehaviour
{
    #region Fields

    //Components
    private Rigidbody rigid;

    //Values
    [Header("Velocity and Force")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    [Header("Capacity")]
    [SerializeField] private int maxJumpCount;
    [SerializeField] private float dodgeTime;
    [SerializeField] private int dodgeCoolTime;

    //for check condition
    private bool canDodge = true;
    private bool canJump = false;

    private bool isDodge = false;
    private int currentJumpCount;
    private float moveSpeed;

    #endregion

    #region Getter

    public bool CanJump { get { return canJump; } }


    public bool CanDodge { get { return canDodge; } }

    #endregion

    #region Funtion and Coroutine

    #region Funtion
    /**
     * @brief 객체를 이동과 회피를 담당하는 메서드
     * @details 매개변수로 이동방향과 달리기 키 입력여부, 회피 키 입력여부를 받아 이동방향으로 설정된 속도만큼 이동시킵니다.\n
     * 회피 중 이동속도는 runSpeed의 2배입니다.
     * 
     * @param[in] directionVec : 이동방향
     * @param[in] runInput : 달리기 키 입력여부
     * @param[in] dodgeInput : 회피 키 입력여부
     * 
     * @author yws
     * @date last change 2022/07/01
     */
    public void MoveEntity(Vector3 directionVec, bool runInput, bool dodgeInput)
    {
        if(!isDodge)
            moveSpeed = runInput ? runSpeed : walkSpeed;

        if (dodgeInput && canDodge)
        {
            isDodge = true;
            canDodge = false;
            moveSpeed = runSpeed * 2;
         
            StartCoroutine(InitDodge());
        }

        transform.LookAt(transform.position + directionVec);
        transform.position += directionVec * moveSpeed * Time.deltaTime;
    }

    /**
     * @brief 객체를 점프시키는 메서드
     * @details 매개변수로 점프키 입력여부를 받아 ture면 점프시킵니다.\n
     * 만약 최대 점프 횟수 이상 점프를 시도하거나 회피 중 시도하는 경우 바로 return합니다.\n\n
     * 
     * 점프 횟수의 초기화는 EntityMovement의 OnCollisionEnter에서 합니다.
     * 
     * @param[in] jumpInput : 점프키 입력여부
     * 
     * @author yws
     * @date last change 2022/06/30
     */
    public void JumpEntity(bool jumpInput)
    {
        if (currentJumpCount == 0 || isDodge)
        {
            canJump = false;
            return;
        }
        if (jumpInput)
        {
            currentJumpCount--;
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    #endregion



    #region Coroutine

    /**
     * @brief 회피를 초기화하는 IEnumerator 입니다.
     * @details 미리 설정해둔 dodge(Time, CollTime)이 지나면 회피를 초기화합니다.
     * 
     * @author yws
     * @date last change 2022/07/01
     */
    IEnumerator InitDodge()
    {
        var wfsID = new WaitForSecondsRealtime(dodgeTime);
        var wfsCD = new WaitForSecondsRealtime(dodgeCoolTime);

        while (true)
        {
            yield return wfsID;
            isDodge = false;

            yield return wfsCD;
            canDodge = true;

            yield break;
        }
    }

    #endregion

    #endregion




    #region Unity Event

    private void Awake()
    {
        currentJumpCount = maxJumpCount;
        TryGetComponent<Rigidbody>(out rigid);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // init jump

        if (collision.collider.CompareTag("Floor"))
        {
            currentJumpCount = maxJumpCount;
            canJump = true;
        }
        // end init jump


    }

    #endregion
}
