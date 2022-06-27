using UnityEngine;

/**
 * @brief class 싱글톤 패턴의 구현을 위한 Singleton 베이스 클래스
 * @details class UpdateManager, GameManager등의 Manager 클래스에 베이스 클래스로 활용되는 Singleton클래스입니다.\n
 * 
 * 사용법\n
 * 만약 Singleton 객체가 파괴될때 처리해야하는 작업이 있다면 OnDestroyed를 override해 그곳에서 처리해줍니다.\n
 * 상속받은 클래스에서는 producted "상속받은 클래스명"() {} 을 반드시 선언해줘야합니다.\n
 * singleton으로 만들 gameObject가 미리 생성되어 있어야 하며 그 오브젝트의 상속받은 클래스가 컴포넌트로 추가되어야 합니다.
 * 
 * @author yws
 * @date 2022/06/26
 */
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    #region Fields

    private static bool isShutDown = false;
    private static object m_Lock = new object();
    private static T instance;

    #endregion

    #region Funtions

    /**
     * @brief instance 체크 메서드
     * @details 현제 instancd의 값을 체크하여 null이면 Scene내에 존재하는 T 타입의 오브젝트를 반환하고 \n
     * 없다면 null을 반환합니다.
     * 
     * @author yws
     * @data last change 2022/06/26
     */
    public static T Instance
    {
        get
        {
            if (isShutDown)
            {
                Debug.LogWarning("[Singleton]?Instance?'" + typeof(T) +
                "'?already?destroyed.?Returning?null.");
                return null;
            }

            lock (m_Lock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                        Debug.LogWarning($"[Singleton <{typeof(T)}>] Can't find the instance of {typeof(T)}. Make sure to create a game object including this component.");
                }

                return instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        isShutDown = true;
    }


    private void OnDestroy()
    {
        if(instance == this)
            isShutDown = true;

        OnDestroyed();
    }

    /**
     * @brief 상속받은 클래스 OnDestroy시 처리해야하는 작업이 있다면 override해서 사용하는 메서드 
     */
    protected virtual void OnDestroyed() { }

    #endregion
}