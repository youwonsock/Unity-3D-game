using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief 
 * @details 
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
     * @brief 
     * @details 
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
     * @brief 
     * @details 
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
