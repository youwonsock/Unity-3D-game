using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class ī�޶� �̵��� ����ϴ� class
 * @details main camera�� �Ҵ��Ͽ� ī�޶� ���� target�� \n
 * camera�� offset�� �������ݴϴ�.
 * 
 * @author yws
 * @date last change 2022/06/29
 */
public class CameraFollow : MonoBehaviour
{
    #region Values

    [SerializeField] Vector3 offset;
    public Transform target;

    #endregion

    #region Funtions

    /**
     * @brief ī�޶� ��ġ�� �������ִ� �޼���
     * @details ī�޶��� ��ġ�� target�� ��ġ + offset���� �������ݴϴ�.
     * UpdateManager�� ����ؼ� ����մϴ�.
     * 
     * @author yws
     * @date last change 2022/06/28
     */
    private void SetCameraPos()
    {
        transform.position = target.position + offset;
    }

    #endregion

    #region Unity Event

    private void OnEnable()
    {
        UpdateManager.SubscribeToLateUpdate(SetCameraPos);
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromLateUpdate(SetCameraPos);
    }

    #endregion
}
