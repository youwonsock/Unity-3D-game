using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * @brief class Entity를 상속받은 적들의 베이스 클래스
 * @details 
 * 
 * @note 현재 Unity의 Nav를 사용하기 때문에 물리력에 의한 추적 방해를 막기 위해 FixedUpdate에서 rigid의 velocity를 vector3.zero로 하고 있습니다.
 *          나중에 다른 방식의 추적을 구현한다면 원래 의도대로 EntityMovement를 사용하여 이동을 구현할 예정입니다.
 * 
 * @author yws
 * @date last change 2022/07/24
 */

public class Enemy : Entity
{
    #region Fields

    [SerializeField] private MeshRenderer[] meshs;
    [SerializeField] protected NavMeshAgent nav;
    [SerializeField] protected Transform target; // find말고 다른 방법?
    [SerializeField] protected bool isChase;  // 현재는 인스팩터에서 수동으로 추적 시작!
    protected EnemyStat enemyStat;

    protected float targetDistance = 100;
    protected bool canAttack = true;

    #endregion



    #region Property

    /**
     * @brief Traget property
     * 
     * @author yws
     * @date last change 2022/08/13
     */
    public Transform Target { get { return target; } set { target = value; } }


    /**
     * @brief Type Getter
     * 
     * @author yws
     * @date last change 2022/08/19
     */
    public EnemyType GetEnemyType { get { return enemyStat.Type; } }

    #endregion



    #region Funtion

    //--------------------------protected--------------------------------------

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

    //--------------------------private--------------------------------------

    /**
     * @brief OnDamaged에서 호출하는 Coroutine
     * 
     * @author yws
     * @date last change 2022/07/23
     */
    IEnumerator OnDamagedCoroutine()
    {
        var wfs = new WaitForSecondsRealtime(0.3f);
        foreach (MeshRenderer mesh in meshs)
            mesh.material.color = Color.red;

        yield return wfs;

        if (Health > 0)
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.white;
        }
        else
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.gray;
        }

        yield break;
    }

    /**
     * @brief Enemy.cs의 UpdateManager.SubscribeToUpdate 구독 메서드
     * @details Update에서 실행해야하는 작업을 구현해두는 메서드입니다.
     * 
     * @author yws
     * @date last change 2022/07/24
     */
    protected virtual void OnUpdateWork()
    {
        if (nav.enabled && target != null)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
    }

    /**
     * @brief Enemy.cs의 UpdateManager.SubscribeToFixedUpdate 구독 메서드
     * @details FixedUpdate에서 실행해야하는 작업을 구현해두는 메서드입니다.
     * 
     * @author yws
     * @date last change 2022/07/24
     */
    protected virtual void OnFixedUpdateWork()
    {
        if (isChase && !isHit)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;

            if(target != null)
                targetDistance = Vector3.Distance(target.position, transform.position);
        }
        if (canAttack && targetDistance < enemyStat.AttackDistance)
        {
            Attack();
        }
    }

    /**
     * @brief Enemy의 공격 메서드
     * @details 각 Enemy의 공격을 구현하는 메서드입니다.\n
     * 자식 클래스에서 override하여 공격을 구현합니다.
     * 
     * @author yws
     * @date last change 2022/08/01
     */
    protected virtual void Attack() { }

    /**
     * @brief Entity의 OnDeath 이벤트에 등록하는 메서드
     * @details 사망시 처리를 구현한 메서드입니다.
     * 
     * @author yws
     * @date last change 2022/07/24
     */
    private void OnDeathWork()
    {
        gameObject.layer = Constants.LayerIdx_Dead;
        animator.SetTrigger("doDie");
        isChase = false;
        nav.enabled = false;

        ItemManager.DropItem(enemyStat.ItemCreateProbability, this.transform);

        switch (enemyStat.Type)
        {
            case EnemyType.A:
                GameManager.Instance.EnemyCountA--;
                break;
            case EnemyType.B:
                GameManager.Instance.EnemyCountB--;
                break;
            case EnemyType.C:
                GameManager.Instance.EnemyCountC--;
                break;
        }

        Destroy(gameObject, 4);
    }

    #endregion



    #region Unity Event

    protected override void Awake()
    {
        // Entity
        base.Awake();

        bool checkComponent = TryGetComponent<IInput>(out input);
        TryGetComponent<EntityMovement>(out movement);
        TryGetComponent<Rigidbody>(out rigid);
        TryGetComponent<NavMeshAgent>(out nav);
        meshs = GetComponentsInChildren<MeshRenderer>();
        transform.GetChild(0).TryGetComponent<Animator>(out animator);

        enemyStat = stat as EnemyStat;
        if (enemyStat == null)
            Debug.Log($"{stat}을 EnemyStat로 캐스팅에 실패하였습니다. {stat}의 타입을 확인해주세요.");

        // OnDeath Event 추가
        OnDeath += OnDeathWork;

        //test용으로 awake에서 anim 변경
        animator.SetBool("isWalk", true);
    }

    protected virtual void OnEnable()
    {
        UpdateManager.SubscribeToUpdate(OnUpdateWork);
        UpdateManager.SubscribeToFixedUpdate(OnFixedUpdateWork);

    }

    protected virtual void OnDisable()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdateWork);
        UpdateManager.UnsubscribeFromFixedUpdate(OnFixedUpdateWork);

        OnDeath -= OnDeathWork;
    }

    #endregion
}