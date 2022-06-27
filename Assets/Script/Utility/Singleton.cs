using UnityEngine;

/**
 * @brief class �̱��� ������ ������ ���� Singleton ���̽� Ŭ����
 * @details class UpdateManager, GameManager���� Manager Ŭ������ ���̽� Ŭ������ Ȱ��Ǵ� SingletonŬ�����Դϴ�.\n
 * 
 * ����\n
 * ���� Singleton ��ü�� �ı��ɶ� ó���ؾ��ϴ� �۾��� �ִٸ� OnDestroyed�� override�� �װ����� ó�����ݴϴ�.\n
 * ��ӹ��� Ŭ���������� producted "��ӹ��� Ŭ������"() {} �� �ݵ�� ����������մϴ�.\n
 * singleton���� ���� gameObject�� �̸� �����Ǿ� �־�� �ϸ� �� ������Ʈ�� ��ӹ��� Ŭ������ ������Ʈ�� �߰��Ǿ�� �մϴ�.
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
     * @brief instance üũ �޼���
     * @details ���� instancd�� ���� üũ�Ͽ� null�̸� Scene���� �����ϴ� T Ÿ���� ������Ʈ�� ��ȯ�ϰ� \n
     * ���ٸ� null�� ��ȯ�մϴ�.
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
     * @brief ��ӹ��� Ŭ���� OnDestroy�� ó���ؾ��ϴ� �۾��� �ִٸ� override�ؼ� ����ϴ� �޼��� 
     */
    protected virtual void OnDestroyed() { }

    #endregion
}