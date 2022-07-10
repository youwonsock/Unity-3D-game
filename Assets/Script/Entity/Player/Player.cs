using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Player의 메인 컴포넌트로 관련된 다른 컴포넌트들을 연결해주는 역할
 * @details input과 movement등의 다른 클래스들의 
 * 
 * @author yws
 * @date last change 2022/07/10
 */
public class Player : Entity
{
    #region Fields

    //SerializeField
    [SerializeField] private int coin;
    [SerializeField] private int maxGrenades;
    [SerializeField] private int grenades;

    //ScriptableObject
    private PlayerStat playerStat; // 아직은 사용 X

    //Player가 가지는 Components
    private PlayerWeapon weapon;

    //values
    private Vector3 nomalVec;
    private bool runInput;
    private bool jumpInput;
    private bool dodgeInput;
    private bool interactionInput;
    private int swapInput;

    #endregion



    #region Property


    /**
     * @brief coin getter
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public int Coin
    {
        get
        {
            return coin;
        }
        set
        {
            coin += value;
        }
    }

    /**
     * @brief maxCoin getter
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public int MaxGrenades { get { return maxGrenades; } }

    /**
     * @brief Grenades getter
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public int Grenades 
    {
        get
        {
            return grenades;
        }
        set
        {
            if (grenades + value > MaxGrenades)
                grenades = maxGrenades;
            else
                grenades += value;
        }
    }

    #endregion



    #region Funtion

    //--------------------------public--------------------------------------

    /**
     * @brief Player 무기 획득 메서드
     * @details Player 무기 획득시 Weapon에서 호출되는 메서드입니다.\n
     * Weapon의 GetWeapon을 호출해서 무기를 변경합니다. (의존성 해결을 위한 구조)
     * @return bool
     * 
     * @author yws
     * @date last change 2022/07/07
     */
    public bool GetWeapon(Weapon.WeaponType itemValue)
    {
         return weapon.GetWeapon(itemValue, interactionInput);
    }

    //--------------------------private-------------------------------------

    /**
     * @brief Player의 Input을 감지하는 메서드
     * @details IInput의 메서드를 사용해서 Player의  bool **Input 값들을 초기화 시킵니다.
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
     * @brief 무기 변경 메서드(Player)
     * @details swapInput을 메개변수로 PlayerWeapon의 Swap을 호출해 해당하는 무기로 변경합니다.
     * swapInput이 0이면 바로 종료합니다.
     * 
     * @author yws
     * @date last change 2022/07/06
     */
    private void SwapWeapon()
    {
        if (swapInput == -1)
            return;

        if(weapon.Swap(swapInput))
            animator.SetTrigger("doSwap");
    }

    /**
     * @brief 플레이어 Animation설정 메서드
     * @details 플레이어의 상태에 따른 Animation을 설정해주는 메서드입니다.
     * 일부 애니메이션 설정은 OnCollisonEnter에서 수행합니다.
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
     * @brief 플레이어 이동 메서드
     * @details 플레이어 이동을 구현한 메서드\n
     * UpdateManager에 등록하여 사용합니다.
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

        playerStat = stat as PlayerStat;
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
