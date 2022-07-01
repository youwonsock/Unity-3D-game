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
     * @brief ��ü�� �̵���Ű�� �޼���
     * @details �Ű������� �̵������ �޸��� Ű �Է¿��θ� �޾� �̵��������� ������ �ӵ���ŭ �̵���ŵ�ϴ�.
     * 
     * @param[in] directionVec : �̵�����
     * @param[in] runInput : �޸��� Ű �Է¿���
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
     * @brief ��ü�� ������Ű�� �޼���
     * @details �Ű������� ����Ű �Է¿��θ� �޾� ture�� ������ŵ�ϴ�.\n
     * ���� �ִ� ���� Ƚ�� �̻� �����õ��� �ٷ� return�մϴ�.\n\n
     * 
     * ���� Ƚ���� �ʱ�ȭ�� EntityMovement�� OnCollisionEnter���� �մϴ�.
     * 
     * @param[in] jumpInput : ����Ű �Է¿���
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
