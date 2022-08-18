using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Player�� ���� ������Ʈ�� ���õ� �ٸ� ������Ʈ���� �������ִ� ����
 * @details input�� movement���� �ٸ� Ŭ�������� Player�� �������̽��� Ȱ���Ͽ� ���� ����մϴ�.
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

    //Player�� ������ Components
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
     * @brief Event Grenades �������� ������ ����ɶ� �ߵ��Ǵ� �̺�Ʈ
     * 
     * @author yws
     * @date last change 2022/07/10
     */
    public event Action ChangeGrenadesCount;

    /**
     * @brief Event GrenadeInput�� True�� ��� �ߵ��Ǵ� �̺�Ʈ
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
     * @details grenadeInput�� true�� �Ǹ� UseGrenade�� ��ϵ� �̺�Ʈ�� �����մϴ�.
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
     * @brief ���� ���� Ÿ�� ��ȯ Property\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public Weapon.WeaponType GetCurrentWeaponType { get { return weapon.GetCurrentWeaponType(); } }

    /**
     * @brief ���� ���� ź�� ��ȯ Property\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public int CurrentWeaponAmmo { get { return weapon.GetCurrentAmmo(); } }

    /**
     * @brief ���� ���� �ִ� ź�� ��ȯ Property\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public int CurrentWeaponMaxAmmo { get { return weapon.GetMaxAmmo(); } }

    /**
     * @brief ���� ������ �ִ� ���� �迭 Property\n
     * 
     * @author yws
     * @date last change 2022/08/11
     */
    public bool[] HasWeapon { get { return weapon.HasWeapon; } }

    #endregion



    #region Funtion

    //--------------------------public--------------------------------------

    /**
     * @brief Player �ʱ�ȭ �޼���
     * @details ����� �� Player �ʱ�ȭ�� ���� �޼����Դϴ�.\n
     * Health, Grenades, HasWeapons[]���� �ʱ�ȭ�մϴ�.
     * 
     * @author yws
     * @date last change 2022/07/07
     */
    public void InitPlayer()
    {
        Coin = 15000; // �ӽ� ��
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
     * @brief Player ���� ȹ�� �޼���
     * @details Player ���� ȹ��� Weapon���� ȣ��Ǵ� �޼����Դϴ�.\n
     * Weapon�� GetWeapon�� ȣ���ؼ� ���⸦ �����մϴ�. (������ �ذ��� ���� ����)
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
     * @brief ��� �� ȣ��Ǵ� �޼���
     * @details Entity�� OnDeath�� �߰��Ͽ� ����� ó���� ���ִ� �޼����Դϴ�.
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
     * @brief �ǰݽ� �ߵ��Ǵ� �ǰ� ó�� �޼���
     * @details �÷��̾ ���Ͱ� ������ �޾��� ��� ����Ǵ� �޼����Դϴ�.\n
     * ������ �ǰ� ó���� �ʿ��� ��� ���� Ŭ�������� override�Ͽ� �� entity���� �ٸ� �ǰ� ȿ���� ������ �� �ֽ��ϴ�.
     * 
     * @author yws
     * @date last change 2022/07/24
     */
    protected override void OnDamaged()
    {
        StartCoroutine(OnDamagedCoroutine());
    }


    /**
     * @brief Player�� Input�� �����ϴ� �޼���
     * @details IInput�� �޼��带 ����ؼ� Player��  bool **Input ������ �ʱ�ȭ ��ŵ�ϴ�.
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
     * @brief ���� ���� �޼���(Player)
     * @details swapInput�� �ް������� PlayerWeapon�� Swap�� ȣ���� �ش��ϴ� ����� �����մϴ�.
     * swapInput�� 0�̸� �ٷ� �����մϴ�.
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
     * @brief �÷��̾� ���� �޼���
     * @details �÷��̾��� AttackInput�� True�� PlayerWeapon�� Attack�� ȣ���մϴ�.\n
     * Attack�� ��ȯ���� �̿��� animation�� �����ŵ�ϴ�.\n
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
     * @brief �÷��̾� ������ �޼���
     * @details PlayerWeapon�� Reload�� ȣ���Ͽ� ���⸦ �������ϴ� �޼����Դϴ�.
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
     * @brief �÷��̾� �̵� �޼���
     * @details �÷��̾ �̵� ��Ű�� �޼����Դϴ�.\n
     * dodgeInput�� true�� ��� SetCanMove�� �̿��Ͽ� DodgeTime ���� �ٸ� ���������� �̵��� ����ϴ�.
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
     * @brief �÷��̾� �ൿ �޼���
     * @details �÷��̾� �ൿ�� ���� ��Ű�� �޼���\n
     * UpdateManager�� ����Ͽ� ����մϴ�.
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
     * @brief player�� �̵����� ���θ� �˻��ϱ� ���� ������ CanMove���� Coroutine
     * @details canMove�� false�� ���� ��\n
     * �Ű������� time��ŭ�� �ð��� ���� �� CanMove�� True�� �ٲپ� �̵��� ���������ϴ�.
     * 
     * @param[in] time : �̵��� ���� �ð�
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
     * @brief OnDamaged���� ȣ���ϴ� Coroutine
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
