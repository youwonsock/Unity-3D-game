using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class 카메라 이동을 담당하는 class
 * @details main camera에 할당하여 카메라가 따라갈 target과 \n
 * camera의 offset을 설정해줍니다.
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
     * @brief 카메라 위치를 설정해주는 메서드
     * @details 카메라의 위치를 target의 위치 + offset으로 변경해줍니다.
     * UpdateManager에 등록해서 사용합니다.
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
