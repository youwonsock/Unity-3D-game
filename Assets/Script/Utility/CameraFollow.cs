using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief 
 * @details 
 * 
 * @author yws
 * @data last change 2022/06/28
 */
public class CameraFollow : MonoBehaviour
{
    #region Values

    [SerializeField] Vector3 offset;
    public Transform target;

    #endregion

    #region Funtions

    /**
     * @brief 
     * @details 
     * 
     * @author yws
     * @data last change 2022/06/28
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
