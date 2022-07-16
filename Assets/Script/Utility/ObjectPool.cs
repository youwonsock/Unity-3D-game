using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @brief class Object Pool�� ������ �̱��� Ŭ����
 * @details ��ü�� Instantiate�� Destroy�� �ٿ��ֱ� ���ؼ� ������ Object Pool�Դϴ�.\n
 * �ٸ� ��ü������ ������ �����ϵ��� �̱������� �����Ͽ����ϴ�.\n
 * ����� �Ѿ˿� ���ؼ��� Object Pooling�� ����մϴ�.\n\n
 * 
 * ���� �ٸ� ��ü���� ����ϰ� �Ǵ� ��� ������ enum�� ����ϴ� ����� �ƴ϶�
 * ���÷��ǰ� ���׸��� ����ϴ� ������� �����Ѵٸ� ������ ���� ���������� ���Ǽ��� ì�� �� ����
 * 
 * ����\n
 * Singleton ��ü�� Object Pool �ٸ� Ŭ�������� �ٷ� ������ �����մϴ�.\n
 * Object Pool�� �����Ͽ� ��ä�� �����մϴ�.
 * 
 * @author yws
 * @date 2022/07/16
 */
public class ObjectPool : Singleton<ObjectPool>
{
    public ObjectPool() { }

    public enum BulletType { HandGun, SMG };

    #region Fields

    [SerializeField] GameObject handGunBulletPrefab;
    [SerializeField] GameObject SMGBulletPrefab;
    [SerializeField] private int bulletCount;

    Queue<Bullet> handGunBulletQueue = new Queue<Bullet>();
    Queue<Bullet> SMGBulletQueue = new Queue<Bullet>();

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief Object Pool �ʱ�ȭ �Լ�
     * @details Object Pool�� �ʱ�ȭ �ϴ� �޼����Դϴ�.\n
     * inspector���� ������ �Ѿ� �� ��ŭ �� queue�� �Ѿ��� �߰��մϴ�.
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    private void Initialize()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            handGunBulletQueue.Enqueue(CreateNewObject(BulletType.HandGun));
            SMGBulletQueue.Enqueue(CreateNewObject(BulletType.SMG));
        }
    }

    /**
     * @brief bullet prefab ���� �Լ�
     * @details queue�� ����� Prefab�� ������ ��� ���ο� ��ü�� �������ֱ� ���� ȣ���ϴ� �޼����Դϴ�.
     * 
     * @return Bullet
     * 
     * @param[in] bulletType : �����ϴ� Bullet�� ����
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    private Bullet CreateNewObject(BulletType bulletType)
    {
        Bullet newObj;

        if(bulletType == BulletType.HandGun)
            newObj = Instantiate(handGunBulletPrefab).GetComponent<Bullet>();
        else
            newObj = Instantiate(SMGBulletPrefab).GetComponent<Bullet>();

        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    /**
     * @brief Object Pool�� ����� ��ü�� �������� �޼���
     * @details �Ű������� ���޵� bulletType�� ���� �ʿ��� �Ѿ��� ��ȯ�ϴ� �޼��� �Դϴ�.\n
     * ����ڴ� ��ȯ ���� ��ü ��� �� ReturnObject�� ȣ���ؾ��մϴ�.
     * 
     * @return Bullet
     * @param[in] bulletType : Bullet�� ����
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    public static Bullet GetObject(BulletType bulletType)
    {
        Queue<Bullet> queue = bulletType == BulletType.HandGun ? Instance.handGunBulletQueue : Instance.SMGBulletQueue;

        if (queue.Count > 0)
        {
            var obj = queue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject(bulletType);
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    /**
     * @brief Object Pool�� ��ü�� ��ȯ�ϴ� �޼���
     * @details �Ű������� ����� ��ü�� Type�� ��ȯ�Ͽ� Object Pool�� �ٽ� �߰��մϴ�.
     * 
     * @param[in] obj : ��ȯ�ϴ� Object
     * @param[in] bulletType : ��ȯ�ϴ� Bullet�� Ÿ��
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    public static void ReturnObject(Bullet obj, BulletType bulletType)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);

        if (bulletType == BulletType.HandGun)
            Instance.handGunBulletQueue.Enqueue(obj);
        else
            Instance.SMGBulletQueue.Enqueue(obj);
    }

    #endregion



    #region Unity Event

    private void Awake()
    {
        Initialize();
    }

    #endregion
}
