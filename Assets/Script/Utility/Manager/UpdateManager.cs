using System;

/**
 * @brief class Update를 한곳에서 수행하도록 해주는 UpdateManager클래스
 * @details class 최적화를 위해서 Update를 각 클래스에서 호출하는것이 아니라 UpdateManager에서 한번에 수행합니다.\n
 * 
 * 사용법\n
 * Singleton 객체인 UpdateManager은 다른 클래스에서 바로 참조가 가능합니다.\n
 * 필요한 Update 처리를 이벤트로 등록하여 사용합니다.
 * 
 * @author yws
 * @date 2022/06/26
 */
public class UpdateManager : Singleton<UpdateManager>
{
    public UpdateManager() { }

    #region Fields
    
    private static event Action OnUpdate;
    private static event Action OnFixedUpdate;
    private static event Action OnLateUpdate;

    #endregion

    #region Funtions

    /**
     * @brief Subscibe(, Fixed, Late)Update <- 이벤트 등록 메서드들
     * @details 이벤트를 등록하는 클래스의 OnEnable에서 Subscribe~ 메서드들을 호출하여 사용합니다.
     * 
     * @author yws
     * @data last change 2022/06/26
     */
    public static void SubscribeToUpdate(Action callback)
    {
        if (Instance == null) 
            return;

        OnUpdate += callback;
    }

    public static void SubscribeToFixedUpdate(Action callback)
    {
        if (Instance == null) 
            return;

        OnFixedUpdate += callback;
    }

    public static void SubscribeToLateUpdate(Action callback)
    {
        if (Instance == null)
            return;

        OnLateUpdate += callback;
    }


    /**
     * @brief Unsubscibe(, Fixed, Late)Update <- 등록된 이벤트 삭제 메서드들
     * @details OnEnable에서 이벤트를 등록한 클래스들의 OnDisable에서 Unsubscribe~ 메서드들을 호출하여 사용합니다.
     * 
     * @author yws
     * @data last change 2022/06/26
     */
    public static void UnsubscribeFromUpdate(Action callback)
    {
        OnUpdate -= callback;
    }

    public static void UnsubscribeFromFixedUpdate(Action callback)
    {
        OnFixedUpdate -= callback;
    }

    public static void UnsubscribeFromLateUpdate(Action callback)
    {
        OnLateUpdate -= callback;
    }

    #endregion

    #region UnityEvent
    void Update()
    {
        if (OnUpdate != null)
            OnUpdate.Invoke();
    }

    private void FixedUpdate()
    {
        if (OnFixedUpdate != null)
            OnFixedUpdate.Invoke();
    }

    private void LateUpdate()
    {
        if (OnLateUpdate != null)
            OnLateUpdate.Invoke();
    }
    #endregion
}
