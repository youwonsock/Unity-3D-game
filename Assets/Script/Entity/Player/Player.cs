using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Player�� ���� ������Ʈ�� ���õ� �ٸ� ������Ʈ���� �������ִ� ����
 * @details input�� movement���� �ٸ� Ŭ�������� 
 * 
 * @author yws
 * @date last change 2022/06/28
 */
public class Player : Entity
{
    #region Fields

    //Components
    private IInput input;
    private EntityMovement movement;
    private Animator animator;

    //values
    private Vector3 nomalVec;
    private bool runInput;
    private bool jumpInput;
    private bool dodgeInput;

    #endregion




    #region Funtion

    /**
     * @brief �÷��̾� �̵� �޼���
     * @details IInput�������̽��� �̿��Ͽ� �Է¿� ���� �÷��̾� �̵��� ������ �޼���\n
     * UpdateManager�� ����Ͽ� ����մϴ�.
     * 
     * @author yws
     * @date last change 2022/07/01
     */
    private void MovePlayer()
    {
        //set values
        nomalVec = input.GetNormalizedVec();
        runInput = input.GetRunInput();
        jumpInput = input.GetJumpInput();
        dodgeInput = input.GetDodgeInput();

        //set animation
        SetPlayerAnimation();

        //move player
        movement.MoveEntity(nomalVec, runInput, dodgeInput);
        movement.JumpEntity(jumpInput);
    }

    /**
     * @brief �÷��̾� Animation���� �޼���
     * @details �÷��̾��� ���¿� ���� Animation�� �������ִ� �޼����Դϴ�.
     * �Ϻ� �ִϸ��̼� ������ OnCollisonEnter���� �����մϴ�.
     * 
     * @author yws
     * @date last change 2022/07/01
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

    #endregion




    #region Unity Evenet

    private void Awake()
    {
        TryGetComponent<IInput>(out input);
        TryGetComponent<EntityMovement>(out movement);
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
        UpdateManager.SubscribeToUpdate(MovePlayer);
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromUpdate(MovePlayer);
    }

    #endregion

}
