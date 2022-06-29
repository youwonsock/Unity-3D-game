using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Player의 메인 컴포넌트로 관련된 다른 컴포넌트들을 연결해주는 역할
 * @details input과 movement등의 다른 클래스들의 
 * 
 * @author yws
 * @data last change 2022/06/28
 */
public class Player : Entity
{
    #region Fields

    //Components
    private PlayerInput input;
    private EntityMovement movement;
    private Animator animator;

    //values
    private Vector3 nomalVec;
    private bool runInput;

    #endregion




    #region Funtion

    /**
     * @brief 플레이어 이동 메서드
     * @details IInput인터페이스를 이용하여 입력에 따른 플레이어 이동을 구현한 메서드\n
     * UpdateManager에 등록하여 사용합니다.
     * 
     * @author yws
     * @data last change 2022/06/28
     */
    private void MovePlayer()
    {
        //set values
        nomalVec = input.GetNormalizedVec();
        runInput = input.GetRunInput();
     
        //move player
        movement.EntityMove(nomalVec, runInput);


        //set animation
        SetPlayerAnimation();
    }

    /**
     * @brief 플레이어 Animation설정 메서드
     * @details 플레이어의 상태에 따른 Animation을 설정해주는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/06/28
     */
    private void SetPlayerAnimation()
    {
        if (runInput)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", nomalVec != Vector3.zero);
        }
    }

    #endregion




    #region Unity Evenet

    private void Awake()
    {
        TryGetComponent<PlayerInput>(out input);
        TryGetComponent<EntityMovement>(out movement);
        transform.GetChild(0).TryGetComponent<Animator>(out animator);
    }


    private void OnEnable()
    {
        UpdateManager.SubscribeToFixedUpdate(MovePlayer);
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromFixedUpdate(MovePlayer);
    }

    #endregion

}
