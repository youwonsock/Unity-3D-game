using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * @brief class ���� �Ѿ� Ŭ����
 * @details Bullet Class�� ����� ���� HomingBullet Class�� NavMeshAgent�� �̿��Ͽ� ���� ����� �����մϴ�.
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
     * @brief target ���� �޼���
     * @details NavMeshAgent�� �̿��Ͽ� target�� �����մϴ�\n
     * UpdateManager�� ����Ͽ� ����մϴ�.
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
