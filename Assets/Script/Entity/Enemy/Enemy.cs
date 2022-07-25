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

    [SerializeField] private Transform target; // find���� �ٸ� ���?
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Material mat;
    [SerializeField] protected bool isChase;  // ����� �ν����Ϳ��� �������� ���� ����!
    protected bool canAttack = true;

    //scriptable object�� ��ü ����
    [SerializeField] protected int damage;
    [SerializeField] protected float attackCooltime;
    [SerializeField] protected float targetDistance = 100;
    //scriptable object�� ��ü ����

    #endregion



    #region Property

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
        mat.color = Color.red;

        yield return wfs;

        if (Health > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;
            Destroy(gameObject, 4);
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
        if (nav.enabled)
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
            targetDistance = Vector3.Distance(target.position, transform.position);
        }
    }

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
        mat = GetComponentInChildren<MeshRenderer>().material;
        transform.GetChild(0).TryGetComponent<Animator>(out animator);

        target = FindObjectOfType<Player>().transform;

        // OnDeath Event �߰�
        OnDeath += OnDeathWork;


        //test������ awake���� anim ����
        animator.SetBool("isWalk", true);
    }

    protected virtual void OnEnable()
    {
        UpdateManager.SubscribeToUpdate(OnUpdateWork);
        UpdateManager.SubscribeToFixedUpdate(OnFixedUpdateWork);

        OnDeath += OnDeathWork;

    }

    protected virtual void OnDisable()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdateWork);
        UpdateManager.UnsubscribeFromFixedUpdate(OnFixedUpdateWork);

        OnDeath -= OnDeathWork;

    }

    #endregion
}