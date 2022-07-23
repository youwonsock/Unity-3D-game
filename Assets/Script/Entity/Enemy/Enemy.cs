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

    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private bool isChase;  // ����� �ν����Ϳ��� �������� ���� ����!

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief Enemy.cs�� UpdateManager.SubscribeToUpdate ���� �޼���
     * @details Update���� �����ؾ��ϴ� �۾��� �����صδ� �޼����Դϴ�.
     * 
     * @author yws
     * @date last change 2022/07/24
     */
    private void OnUpdateWork()
    {
        if(isChase && !isHit)
            nav.SetDestination(target.position);
    }

    /**
     * @brief Enemy.cs�� UpdateManager.SubscribeToFixedUpdate ���� �޼���
     * @details FixedUpdate���� �����ؾ��ϴ� �۾��� �����صδ� �޼����Դϴ�.
     * 
     * @author yws
     * @date last change 2022/07/24
     */
    private void OnFixedUpdateWork()
    {
        if (isChase && !isHit)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
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
    }

    private void OnEnable()
    {
        UpdateManager.SubscribeToUpdate(OnUpdateWork);
        UpdateManager.SubscribeToFixedUpdate(OnFixedUpdateWork);
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdateWork);
        UpdateManager.UnsubscribeFromFixedUpdate(OnFixedUpdateWork);
    }

    #endregion
}