using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Entity ��ü���� �̵��� ������ Ŭ����
 * @details Entity ��ü���� �������� �̵��� ������ Ŭ�����Դϴ�.\n
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
     * @brief ���콺�� ���� ȸ�� �޼���
     * @details �Ű������� ���콺 ��ġ vector3�� �޾ƿ� ��ü�� ȸ����ŵ�ϴ�.\n
     * 
     * @param[in] directionVec : ���콺 Ŀ�� ���� ����
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
            rigid.AddForce(Vector3.up * stat.JumpForce, ForceMode.Impulse);
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
