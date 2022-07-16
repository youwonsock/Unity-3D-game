using System;

/**
 * @brief class Update�� �Ѱ����� �����ϵ��� ���ִ� UpdateManagerŬ����
 * @details class ����ȭ�� ���ؼ� Update�� �� Ŭ�������� ȣ���ϴ°��� �ƴ϶� UpdateManager���� �ѹ��� �����մϴ�.\n
 * 
 * ����\n
 * Singleton ��ü�� UpdateManager�� �ٸ� Ŭ�������� �ٷ� ������ �����մϴ�.\n
 * �ʿ��� Update ó���� �̺�Ʈ�� ����Ͽ� ����մϴ�.
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
     * @brief Subscibe(, Fixed, Late)Update <- �̺�Ʈ ��� �޼����
     * @details �̺�Ʈ�� ����ϴ� Ŭ������ OnEnable���� Subscribe~ �޼������ ȣ���Ͽ� ����մϴ�.
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
     * @brief Unsubscibe(, Fixed, Late)Update <- ��ϵ� �̺�Ʈ ���� �޼����
     * @details OnEnable���� �̺�Ʈ�� ����� Ŭ�������� OnDisable���� Unsubscribe~ �޼������ ȣ���Ͽ� ����մϴ�.
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
