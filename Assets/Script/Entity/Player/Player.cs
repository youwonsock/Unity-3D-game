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

    //Player가 가지는 Components
    private PlayerWeapon weapon;
    MeshRenderer[] meshs;

    //input fileds
    private Vector3 nomalVec;
    private bool runInput;
    private bool jumpInput;
    private bool dodgeInput;
    private bool interactionInput;
    private bool attackInput;
    private bool grenadeInput;
    private bool reloadInput;
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

    /**
     * @brief Event GrenadeInput이 True인 경우 발동되는 이벤트
     * 
     * @author yws
     * @date last change 2022/07/10
     */
    public event Action<Vector3> UseGrenade;

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
            coin = Mathf.Clamp(value, 0,Constants.INF);
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
            grenades = Mathf.Clamp(value, 0, maxGrenades);

            ChangeGrenadesCount?.Invoke();
        }
    }

    /**
     * @brief GrenadeInput Property\n
     * @details grenadeInput이 true가 되면 UseGrenade에 등록된 이벤트를 실행합니다.
     * 
     * @author yws
     * @date last change 2022/07/23
     */
    public bool GrenadeInput 
    {
        set 
        { 
            grenadeInput = value;
            if (grenadeInput && !reloadInput && !dodgeInput)
                UseGrenade?.Invoke(input.GetMouseTrunVec());
        }
    }

    /**
     * @brief interactionInput Getter\n
     * 
     * @author yws
     * @date last change 2022/08/02
     */
    public bool InteractionInput { get { return interactionInput; } }


    /**
     * @brief CanMove Property\n
     * 
     * @author yws
     * @date last change 2022/08/02
     */
    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }

    //-------------------------For UI------------------------------------------------

    /**
     * @brief 현재 무기 타입 반환 Property\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public Weapon.WeaponType GetCurrentWeaponType { get { return weapon.GetCurrentWeaponType(); } }

    /**
     * @brief 현재 무기 탄약 반환 Property\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public int CurrentWeaponAmmo { get { return weapon.GetCurrentAmmo(); } }

    /**
     * @brief 현재 무기 최대 탄약 반환 Property\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public int CurrentWeaponMaxAmmo { get { return weapon.GetMaxAmmo(); } }

    /**
     * @brief 현재 가지고 있는 무기 배열 Property\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public bool[] HasWeapon { get { return weapon.HasWeapon; } }

    #endregion



    #region Funtion

    //--------------------------public--------------------------------------

    /**
     * @brief Player 초기화 메서드
     * @details 재시작 시 Player 초기화를 위한 메서드입니다.\n
     * Health, Grenades, HasWeapons[]등을 초기화합니다.
     * 
     * @author yws
     * @date last change 2022/07/07
     */
    public void InitPlayer()
    {
        Coin = 15000; // 임시 값
        Health = MaxHealth;
        Grenades = 0;
        canMove = true;
        isHit = false;
        isDead = false;

        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }
        weapon.InitPlayerWeapons();

        this.gameObject.SetActive(false);
    }

    /**
     * @brief Player 무기 획득 메서드
     * @details Player 무기 획득시 Weapon에서 호출되는 메서드입니다.\n
     * Weapon의 GetWeapon을 호출해서 무기를 변경합니다. (의존성 해결을 위한 구조)
     * @return bool
     * 
     * @author yws
     * @date last change 2022/07/07
     */
    public bool GetWeapon(Weapon.WeaponType itemValue, bool autoGet = false)
    {
        if(!autoGet)
            return weapon.GetWeapon(itemValue, interactionInput);
        else
            return weapon.GetWeapon(itemValue, true);
    }

    //--------------------------private-------------------------------------

    /**
     * @brief 사망 시 호출되는 메서드
     * @details Entity의 OnDeath에 추가하여 사망시 처리를 해주는 메서드입니다.
     * 
     * @author yws
     * @date last change 2022/08/14
     */
    private void OnDeathWork()
    {
        animator.SetTrigger("doDie");
        canMove = false;
        isHit = false;

        nomalVec = Vector3.zero;
        attackInput = false;

        GameManager.Instance.GameOver();
    }

    /**
     * @brief 피격시 발동되는 피격 처리 메서드
     * @details 플레이어나 몬스터가 공격을 받았을 경우 실행되는 메서드입니다.\n
     * 별도의 피격 처리가 필요한 경우 하위 클래스에서 override하여 각 entity마다 다른 피격 효과를 구현할 수 있습니다.
     * 
     * @author yws
     * @date last change 2022/07/24
     */
    protected override void OnDamaged()
    {
        StartCoroutine(OnDamagedCoroutine());
    }


    /**
     * @brief Player의 Input을 감지하는 메서드
     * @details IInput의 메서드를 사용해서 Player의  bool **Input 값들을 초기화 시킵니다.
     * 
     * @author yws
     * @date last change 2022/07/13
     */
    private void GetPlayerInput()
    {
        if (canMove)
        {
            interactionInput = input.GetInteractInput();
            nomalVec = input.GetNormalizedVec();
            attackInput = input.GetAttackInput();
            dodgeInput = input.GetDodgeInput();
            jumpInput = input.GetJumpInput();
            runInput = input.GetRunInput();
            swapInput = input.GetSwapInput();
            reloadInput = input.GetReloadInput();
            GrenadeInput = input.GetGrenadeInput();
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
                animator.SetTrigger("doShot");
                break;
            case 2:
                animator.SetTrigger("doShot");
                break;
        }
    }

    /**
     * @brief 플레이어 재장전 메서드
     * @details PlayerWeapon의 Reload를 호출하여 무기를 재장전하는 메서드입니다.
     * 
     * @author yws
     * @date last change 2022/07/17
     */
    private void ReloadWeapon()
    {
        if (reloadInput)
            if (weapon.Reload())
                animator.SetTrigger("doReload");
    }

    /**
     * @brief 플레이어 이동 메서드
     * @details 플레이어를 이동 시키는 메서드입니다.\n
     * dodgeInput이 true일 경우 SetCanMove를 이용하여 DodgeTime 동안 다른 방향으로의 이동을 멈춤니다.
     * 
     * @author yws
     * @date last change 2022/07/13
     */
    private void MovePlayer()
    {
        if(dodgeInput)
            StartCoroutine(SetCanMove(movement.DodgeTime));

        movement.MoveEntity(nomalVec, runInput, dodgeInput);
        if (attackInput)
        {
            movement.TurnByMouse(input.GetMouseTrunVec());
        }
        movement.JumpEntity(jumpInput);
    }

    /**
     * @brief 플레이어 행동 메서드
     * @details 플레이어 행동을 실행 시키는 메서드\n
     * UpdateManager에 등록하여 사용합니다.
     * 
     * @author yws
     * @date last change 2022/07/13
     */
    private void ActPlayer()
    {

        GetPlayerInput();

        // set weapon
        AttackWeapon();
        SwapWeapon();
        ReloadWeapon();

        // set animation
        SetPlayerAnimation();

        // move player
        MovePlayer();
        
    }

    /**
     * @brief player의 이동가능 여부를 검사하기 위한 변수인 CanMove설정 Coroutine
     * @details canMove를 false로 설정 후\n
     * 매개변수인 time만큼의 시간이 지난 뒤 CanMove를 True로 바꾸어 이동이 가능해집니다.
     * 
     * @param[in] time : 이동을 막을 시간
     * 
     * @author yws
     * @date last change 2022/07/13
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


    /**
     * @brief OnDamaged에서 호출하는 Coroutine
     * 
     * @author yws
     * @date last change 2022/07/24
     */
    IEnumerator OnDamagedCoroutine()
    {
        isHit = true;

        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow;
        }

        yield return new WaitForSecondsRealtime(1f);


        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }
        isHit = false;

        yield break;
    }

    #endregion




    #region Unity Evenet

    protected override void Awake()
    {
        // Entity
        base.Awake();

        bool checkComponent = TryGetComponent<IInput>(out input);
        TryGetComponent<EntityMovement>(out movement);
        TryGetComponent<Rigidbody>(out rigid);
        transform.GetChild(0).TryGetComponent<Animator>(out animator);

        // Player
        TryGetComponent<PlayerWeapon>(out weapon);
        meshs = GetComponentsInChildren<MeshRenderer>();
        OnDeath += OnDeathWork;

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
