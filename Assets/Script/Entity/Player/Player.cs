using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Player의 메인 컴포넌트로 관련된 다른 컴포넌트들을 연결해주는 역할
 * @details input과 movement등의 다른 클래스들의 
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

    #endregion




    #region Funtion

    /**
     * @brief 플레이어 이동 메서드
     * @details IInput인터페이스를 이용하여 입력에 따른 플레이어 이동을 구현한 메서드\n
     * UpdateManager에 등록하여 사용합니다.
     * 
     * @author yws
     * @date last change 2022/06/28
     */
    private void MovePlayer()
    {
        //set values
        nomalVec = input.GetNormalizedVec();
        runInput = input.GetRunInput();
        jumpInput = input.GetJumpInput();

        //set animation
        SetPlayerAnimation();

        //move player
        movement.MoveEntity(nomalVec, runInput);
        movement.JumpEntity(jumpInput);
    }

    /**
     * @brief 플레이어 Animation설정 메서드
     * @details 플레이어의 상태에 따른 Animation을 설정해주는 메서드입니다.
     * 일부 애니메이션 설정은 OnCollisonEnter에서 수행합니다.
     * 
     * @author yws
     * @date last change 2022/06/28
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
        if (movement.CurrentJumpCount != 0 && jumpInput)
        {
            animator.SetTrigger("doJump");
            animator.SetBool("isJump", true);
        }
        // end jump anim
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
