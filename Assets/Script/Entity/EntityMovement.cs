using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Entity ��ü���� �̵��� ������ Ŭ����
 * @details Entity ��ü���� �������� �̵��� ������ Ŭ�����Դϴ�.\n
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
     * @brief ��ü�� �̵��� ȸ�Ǹ� ����ϴ� �޼���
     * @details �Ű������� �̵������ �޸��� Ű �Է¿���, ȸ�� Ű �Է¿��θ� �޾� �̵��������� ������ �ӵ���ŭ �̵���ŵ�ϴ�.\n
     * ȸ�� �� �̵��ӵ��� runSpeed�� 2���Դϴ�.
     * 
     * @param[in] directionVec : �̵�����
     * @param[in] runInput : �޸��� Ű �Է¿���
     * @param[in] dodgeInput : ȸ�� Ű �Է¿���
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
     * @brief ��ü�� ������Ű�� �޼���
     * @details �Ű������� ����Ű �Է¿��θ� �޾� ture�� ������ŵ�ϴ�.\n
     * ���� �ִ� ���� Ƚ�� �̻� ������ �õ��ϰų� ȸ�� �� �õ��ϴ� ��� �ٷ� return�մϴ�.\n\n
     * 
     * ���� Ƚ���� �ʱ�ȭ�� EntityMovement�� OnCollisionEnter���� �մϴ�.
     * 
     * @param[in] jumpInput : ����Ű �Է¿���
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
     * @brief ȸ�Ǹ� �ʱ�ȭ�ϴ� IEnumerator �Դϴ�.
     * @details �̸� �����ص� dodge(Time, CollTime)�� ������ ȸ�Ǹ� �ʱ�ȭ�մϴ�.
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
