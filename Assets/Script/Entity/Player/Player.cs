using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Player�� ���� ������Ʈ�� ���õ� �ٸ� ������Ʈ���� �������ִ� ����
 * @details input�� movement���� �ٸ� Ŭ�������� 
 * 
 * @author yws
 * @date last change 2022/07/07
 */
public class Player : Entity
{
    #region Fields

    //Components
    private IInput input;
    private EntityMovement movement;
    private PlayerWeapon weapon;
    private Animator animator;

    //values
    private Vector3 nomalVec;
    private bool runInput;
    private bool jumpInput;
    private bool dodgeInput;
    private bool interactionInput;
    private int swapInput;

    #endregion



    #region Property

    public bool InteractionInput() { return interactionInput; }

    #endregion



    #region Funtion

    /**
     * @brief Player�� Input�� �����ϴ� �޼���
     * @details IInput�� �޼��带 ����ؼ� Player��  bool **Input ������ �ʱ�ȭ ��ŵ�ϴ�.
     * 
     * @author yws
     * @date last change 2022/07/06
     */
    private void GetPlayerInput()
    {
        nomalVec = input.GetNormalizedVec();
        runInput = input.GetRunInput();
        jumpInput = input.GetJumpInput();
        dodgeInput = input.GetDodgeInput();
        interactionInput = input.GetInteractInput();
        swapInput = input.GetSwapInput();
    }

    /**
     * @brief ���� ���� �޼���(Player)
     * @details swapInput�� ���� ���� ��ȣ�� �ش��ϴ� ����� �����մϴ�.
     * swapInput�� 0�̸� �ٷ� �����մϴ�.
     * 
     * @author yws
     * @date last change 2022/07/06
     */
    private void SwapWeapon()
    {
        if (swapInput == 0)
            return;

        weapon.Swap(swapInput);
        animator.SetTrigger("doSwap");
    }

    /**
     * @brief �÷��̾� Animation���� �޼���
     * @details �÷��̾��� ���¿� ���� Animation�� �������ִ� �޼����Դϴ�.
     * �Ϻ� �ִϸ��̼� ������ OnCollisonEnter���� �����մϴ�.
     * 
     * @author yws
     * @date last change 2022/07/03
     */
    private void SetPlayerAnimation()
    {
        // run anim set
        if (runInput)
            animator.SetBool("isRun", true);
        else
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", nomalVec != Vector3.zero);
        }
        // end run anim set

        // jump anim set
        if (movement.CanJump && jumpInput)
        {
            animator.SetTrigger("doJump");
            animator.SetBool("isJump", true);
        }
        // end jump anim

        // dodge anim set
        if (movement.CanDodge && dodgeInput)
            animator.SetTrigger("doDodge");
        // end dodge anim set
    }

    /**
     * @brief �÷��̾� �̵� �޼���
     * @details �÷��̾� �̵��� ������ �޼���\n
     * UpdateManager�� ����Ͽ� ����մϴ�.
     * 
     * @author yws
     * @date last change 2022/07/06
     */
    private void ActPlayer()
    {
        GetPlayerInput();

        SwapWeapon();

        //set animation
        SetPlayerAnimation();

        //move player
        movement.MoveEntity(nomalVec, runInput, dodgeInput);
        movement.JumpEntity(jumpInput);
    }

    #endregion




    #region Unity Evenet

    private void Awake()
    {
        TryGetComponent<IInput>(out input);
        TryGetComponent<EntityMovement>(out movement);
        TryGetComponent<PlayerWeapon>(out weapon);
        transform.GetChild(0).TryGetComponent<Animator>(out animator);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // jump anim set
        if (collision.collider.CompareTag("Floor"))
            animator.SetBool("isJump", false);
        // end jump anim
    }


    private void OnEnable()
    {
        UpdateManager.SubscribeToUpdate(ActPlayer);
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromUpdate(ActPlayer);
    }

    #endregion

}
