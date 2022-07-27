using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * @brief class 유도 총알 클래스
 * @details Bullet Class의 상속을 받은 HomingBullet Class는 NavMeshAgent를 이용하여 유도 기능을 구현합니다.
 * 
 * 
 * @author yws
 * @date 2022/07/28
 */
public class HomingBullet : Bullet
{
    #region Fields

    [SerializeField] Transform target;
    NavMeshAgent nav;

    #endregion



    #region Funtion

    /**
     * @brief target 추적 메서드
     * @details NavMeshAgent를 이용하여 target을 추적합니다\n
     * UpdateManager에 등록하여 사용합니다.
     * 
     * @author yws
     * @data 2022/07/28
     */
    private void HomingTarget()
    {
        nav.SetDestination(target.position);
    }

    #endregion



    #region Unity Event

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent<NavMeshAgent>(out nav);
        target = FindObjectOfType<Player>().transform;
    }

    private void OnEnable()
    {
        UpdateManager.SubscribeToUpdate(HomingTarget);
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromUpdate(HomingTarget);
    }

    #endregion
}
