using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Entity 객체들의 이동을 구현한 클레스
 * @details Entity 객체들의 실질적인 이동을 구현한 클래스입니다.\n
 * 
 * @author yws
 * @date last change 2022/07/22
 */
public class EntityMovement : MonoBehaviour
{
    #region Fields

    //Components
    private Rigidbody rigid;

    //Values
    [SerializeField]private PlayerStat stat;

    //for check condition
    private bool canDodge = true;
    private bool canJump = false;

    //fields
    private bool isDodge = false;
    private int currentJumpCount;
    private float moveSpeed;

    #endregion

    #region Property

    public bool CanJump { get { return canJump; } }

    public bool CanDodge { get { return canDodge; } }

    public bool IsDodge { get { return isDodge; } }

    public float DodgeTime { get { return stat.DodgeTime; } }

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
            moveSpeed = runInput ? stat.RunSpeed : stat.WalkSpeed;

        if (dodgeInput && canDodge)
        {
            isDodge = true;
            canDodge = false;
            moveSpeed = stat.RunSpeed * 2;
         
            StartCoroutine(InitDodge());
        }

        transform.LookAt(transform.position + directionVec);

        if (!Physics.Raycast(transform.position, directionVec, 5, Constants.LayerMaskNum_Wall))
            transform.position += directionVec * moveSpeed * Time.deltaTime;
    }


    /**
     * @brief 마우스에 의한 회전 메서드
     * @details 매개변수로 마우스 위치 vector3를 받아와 객체를 회전시킵니다.\n
     * 
     * @param[in] directionVec : 마우스 커서 방향 벡터
     * 
     * @author yws
     * @date last change 2022/07/18
     */
    public void TurnByMouse(Vector3 directionVec)
    {
        if(!isDodge)
            transform.LookAt(transform.position + directionVec);
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
            rigid.AddForce(Vector3.up * stat.JumpForce, ForceMode.Impulse);
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
        var wfsID = new WaitForSecondsRealtime(stat.DodgeTime);
        var wfsCD = new WaitForSecondsRealtime(stat.DodgeCoolTime);

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
        currentJumpCount = stat.MaxJumpCount;
        TryGetComponent<Rigidbody>(out rigid);

        if (!rigid)
            Debug.Log($"Some Component is null : {this.name} .EntityMovement.cs");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // init jump

        if (collision.collider.CompareTag("Floor"))
        {
            currentJumpCount = stat.MaxJumpCount;
            canJump = true;
        }
        // end init jump


    }

    #endregion
}
