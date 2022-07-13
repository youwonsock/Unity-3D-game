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

    //ScriptableObject
    private PlayerStat playerStat; // ������ ��� X

    //Player�� ������ Components
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
     * @brief Event Grenades �������� ������ ����ɶ� �ߵ��Ǵ� �̺�Ʈ
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
     * @brief Player ���� ȹ�� �޼���
     * @details Player ���� ȹ��� Weapon���� ȣ��Ǵ� �޼����Դϴ�.\n
     * Weapon�� GetWeapon�� ȣ���ؼ� ���⸦ �����մϴ�. (������ �ذ��� ���� ����)
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
     * @brief Player�� Input�� �����ϴ� �޼���
     * @details IInput�� �޼��带 ����ؼ� Player��  bool **Input ������ �ʱ�ȭ ��ŵ�ϴ�.
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
                break;
            case 2:
                break;
        }
    }

    /**
     * @brief �÷��̾� �̵� �޼���
     * @details �÷��̾ �̵� ��Ű�� �޼����Դϴ�.\n
     * dodgeInput�� true�� ��� SetCanMove�� �̿��Ͽ� DodgeTime ���� �ٸ� ���������� �̵��� ����ϴ�.
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
     * @brief �÷��̾� �ൿ �޼���
     * @details �÷��̾� �ൿ�� ���� ��Ű�� �޼���\n
     * UpdateManager�� ����Ͽ� ����մϴ�.
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
     * @brief player�� �̵����� ���θ� �˻��ϱ� ���� ������ CanMove���� �ڷ�ƾ
     * @details canMove�� false�� ���� ��\n
     * �Ű������� time��ŭ�� �ð��� ���� �� CanMove�� True�� �ٲپ� �̵��� ���������ϴ�.
     * 
     * @param[in] time : �̵��� ���� �ð�
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
        // input�� Null
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
