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

    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private bool isChase;  // 현재는 인스팩터에서 수동으로 추적 시작!

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief Enemy.cs의 UpdateManager.SubscribeToUpdate 구독 메서드
     * @details Update에서 실행해야하는 작업을 구현해두는 메서드입니다.
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
     * @brief Enemy.cs의 UpdateManager.SubscribeToFixedUpdate 구독 메서드
     * @details FixedUpdate에서 실행해야하는 작업을 구현해두는 메서드입니다.
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

        // OnDeath Event 추가
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