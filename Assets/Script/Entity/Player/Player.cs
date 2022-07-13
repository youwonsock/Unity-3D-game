using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Player의 메인 컴포넌트로 관련된 다른 컴포넌트들을 연결해주는 역할
 * @details input과 movement등의 다른 클래스들이 Player의 인터페이스를 활용하여 서로 통신합니다.
 * 
 * @author yws
 * @date last change 2022/07/13
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

    //input fileds
    private Vector3 nomalVec;
    private bool runInput;
    private bool jumpInput;
    private bool dodgeInput;
    private bool interactionInput;
    private bool attackInput;
    private int swapInput;


    //for check condition
    private bool canMove = true;

    #endregion




    #region Event

    /**
     * @brief Event Grenades 아이템의 개수가 변경될때 발동되는 이벤트
     * 
     * @author yws
     * @date last change 2022/07/10
     */
    public event Action ChangeGrenadesCount;

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

            ChangeGrenadesCount.Invoke();
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
        if (canMove)
        {
            nomalVec = input.GetNormalizedVec();
            attackInput = input.GetAttackInput();
            dodgeInput = input.GetDodgeInput();
            jumpInput = input.GetJumpInput();
            runInput = input.GetRunInput();
            interactionInput = input.GetInteractInput();
            swapInput = input.GetSwapInput();
        }
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
     * @brief 플레이어 공격 메서드
     * @details 플레이어의 AttackInput이 True면 PlayerWeapon의 Attack을 호출합니다.\n
     * Attack의 반환값을 이용해 animation을 실행시킵니다.\n
     * 0 : hammer, 1 : SMG, 2 : HandGun, -1 : return
     * 
     * @author yws
     * @date last change 2022/07/13
     */
    private void AttackWeapon()
    {
        if (!attackInput)
            return;

        switch (weapon.Attack())
        {
            case -1:
                return;
            case 0:
                animator.SetTrigger("doSwing");
                nomalVec = Vector3.zero;
                StartCoroutine(SetCanMove(weapon.CastingTime));
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    /**
     * @brief 플레이어 이동 메서드
     * @details 플레이어를 이동 시키는 메서드입니다.\n
     * dodgeInput이 true일 경우 SetCanMove를 이용하여 DodgeTime 동안 다른 방향으로의 이동을 멈춤니다.
     * 
     * @author yws
     * @date last change 2022/07/06
     */
    private void MovePlayer()
    {
        if(dodgeInput)
            StartCoroutine(SetCanMove(movement.DodgeTime));

        movement.MoveEntity(nomalVec, runInput, dodgeInput);
        movement.JumpEntity(jumpInput);
    }

    /**
     * @brief 플레이어 행동 메서드
     * @details 플레이어 행동을 실행 시키는 메서드\n
     * UpdateManager에 등록하여 사용합니다.
     * 
     * @author yws
     * @date last change 2022/07/06
     */
    private void ActPlayer()
    {

        GetPlayerInput();

        // set weapon 
        AttackWeapon();
        SwapWeapon();

        // set animation
        SetPlayerAnimation();

        // move player
        MovePlayer();
    }

    /**
     * @brief player의 이동가능 여부를 검사하기 위한 변수인 CanMove설정 코루틴
     * @details canMove를 false로 설정 후\n
     * 매개변수인 time만큼의 시간이 지난 뒤 CanMove를 True로 바꾸어 이동이 가능해집니다.
     * 
     * @param[in] time : 이동을 막을 시간
     * 
     * @author yws
     * @date last change 2022/07/01
     */
    IEnumerator SetCanMove(float time)
    {
        canMove = false;
        while (true)
        {
            yield return new WaitForSeconds(time);
            canMove = true;

            yield break;
        }
    }

    #endregion




    #region Unity Evenet

    private void Awake()
    {
        // input은 Null
        bool checkComponent = TryGetComponent<IInput>(out input);
        TryGetComponent<EntityMovement>(out movement);
        TryGetComponent<PlayerWeapon>(out weapon);
        transform.GetChild(0).TryGetComponent<Animator>(out animator);

        playerStat = stat as PlayerStat;

        if (!checkComponent || !movement || !weapon || !animator)
            Debug.Log($"Some Component is null : {this.name} .Player.cs");
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
