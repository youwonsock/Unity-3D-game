using System;

public class UpdateManager : Singleton<UpdateManager>
{
    protected UpdateManager() { }

    #region Fields
    
    private static event Action OnUpdate;
    private static event Action OnFixedUpdate;
    private static event Action OnLateUpdate;

    #endregion

    #region Funtions

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
