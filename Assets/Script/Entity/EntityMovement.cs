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
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    [Header("Capacity")]
    [SerializeField] private int maxJumpCount;
    [SerializeField] private float dodgeTime;
    [SerializeField] private int dodgeCoolTime;

    //for check condition
    private bool canDodge = true;
    private int currentJumpCount;

    #endregion

    #region Getter

    public int CurrentJumpCount { get { return currentJumpCount; } }

    public int MaxJumpCount { get { return maxJumpCount; } }

    #endregion

    #region Funtion and Coroutine

    #region Funtion
    /**
     * @brief 객체를 이동시키는 메서드
     * @details 매개변수로 이동방향과 달리기 키 입력여부를 받아 이동방향으로 설정된 속도만큼 이동시킵니다.
     * 
     * @param[in] directionVec : 이동방향
     * @param[in] runInput : 달리기 키 입력여부
     * 
     * @author yws
     * @date last change 2022/06/28
     */
    public void MoveEntity(Vector3 directionVec, bool runInput)
    {
        transform.LookAt(transform.position + directionVec);
        transform.position += directionVec * (runInput ? runSpeed : speed)* Time.deltaTime;
    }

    /**
     * @brief 객체를 점프시키는 메서드
     * @details 매개변수로 점프키 입력여부를 받아 ture면 점프시킵니다.\n
     * 만약 최대 점프 횟수 이상 점프시도시 바로 return합니다.\n\n
     * 
     * 점프 횟수의 초기화는 EntityMovement의 OnCollisionEnter에서 합니다.
     * 
     * @param[in] jumpInput : 점프키 입력여부
     * 
     * @author yws
     * @date last change 2022/06/230
     */
    public void JumpEntity(bool jumpInput)
    {
        if (currentJumpCount == 0)
            return;
        if (jumpInput)
        {
            currentJumpCount--;
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /**
     * 
     */
    public void DodgeEntity(Vector3 directionVec,bool dodgeInput)
    {
        if(canDodge && dodgeInput)
        {
            speed *= 2;
            runSpeed *= 2;

            canDodge = false;

            StartCoroutine(InitDodge());
        }
    }
    #endregion



    #region Coroutine

    /**
     * 
     */
    IEnumerator InitDodge()
    {
        var wfsID = new WaitForSecondsRealtime(dodgeTime);
        var wfsCD = new WaitForSecondsRealtime(dodgeCoolTime);

        while (true)
        {
            yield return wfsID;
            speed /= 2;
            runSpeed /=2;

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
            currentJumpCount = maxJumpCount;

        // end init jump


    }

    #endregion
}
