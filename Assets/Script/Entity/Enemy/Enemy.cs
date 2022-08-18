using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * @brief class Entity�� ��ӹ��� ������ ���̽� Ŭ����
 * @details 
 * 
 * @note ���� Unity�� Nav�� ����ϱ� ������ �����¿� ���� ���� ���ظ� ���� ���� FixedUpdate���� rigid�� velocity�� vector3.zero�� �ϰ� �ֽ��ϴ�.
 *          ���߿� �ٸ� ����� ������ �����Ѵٸ� ���� �ǵ���� EntityMovement�� ����Ͽ� �̵��� ������ �����Դϴ�.
 * 
 * @author yws
 * @date last change 2022/07/24
 */

public class Enemy : Entity
{
    #region Fields

    [SerializeField] private MeshRenderer[] meshs;
    [SerializeField] protected NavMeshAgent nav;
    [SerializeField] protected Transform target; // find���� �ٸ� ���?
    [SerializeField] protected bool isChase;  // ����� �ν����Ϳ��� �������� ���� ����!
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

    //--------------------------private--------------------------------------

    /**
     * @brief OnDamaged���� ȣ���ϴ� Coroutine
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
     * @brief Enemy.cs�� UpdateManager.SubscribeToUpdate ���� �޼���
     * @details Update���� �����ؾ��ϴ� �۾��� �����صδ� �޼����Դϴ�.
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
     * @brief Enemy.cs�� UpdateManager.SubscribeToFixedUpdate ���� �޼���
     * @details FixedUpdate���� �����ؾ��ϴ� �۾��� �����صδ� �޼����Դϴ�.
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
     * @brief Enemy�� ���� �޼���
     * @details �� Enemy�� ������ �����ϴ� �޼����Դϴ�.\n
     * �ڽ� Ŭ�������� override�Ͽ� ������ �����մϴ�.
     * 
     * @author yws
     * @date last change 2022/08/01
     */
    protected virtual void Attack() { }

    /**
     * @brief Entity�� OnDeath �̺�Ʈ�� ����ϴ� �޼���
     * @details ����� ó���� ������ �޼����Դϴ�.
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
            Debug.Log($"{stat}�� EnemyStat�� ĳ���ÿ� �����Ͽ����ϴ�. {stat}�� Ÿ���� Ȯ�����ּ���.");

        // OnDeath Event �߰�
        OnDeath += OnDeathWork;

        //test������ awake���� anim ����
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