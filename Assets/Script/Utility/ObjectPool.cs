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
 * @date 2022/07/28
 */
public class ObjectPool : Singleton<ObjectPool>
{
    public ObjectPool() { }

    public enum BulletType { HandGun, SMG, Missile, BossMissile, BossRock };

    #region Fields

    [SerializeField] GameObject handGunBulletPrefab;
    [SerializeField] GameObject SMGBulletPrefab;
    [SerializeField] GameObject missileBulletPrefab;
    [SerializeField] GameObject bossMissileBulletPrefab;
    [SerializeField] GameObject bossRockBulletPrefab;

    [SerializeField] private int playerBulletCount;
    [SerializeField] private int missileBulletCount;
    [SerializeField] private int bossBulletCount;

    Queue<Bullet> handGunBulletQueue = new Queue<Bullet>();
    Queue<Bullet> SMGBulletQueue = new Queue<Bullet>();
    Queue<Bullet> missileBulletQueue = new Queue<Bullet>();
    Queue<Bullet> bossMissileBulletQueue = new Queue<Bullet>();
    Queue<Bullet> bossRockBulletQueue = new Queue<Bullet>();

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
     * @date last change 2022/07/28
     */
    private void Initialize()
    {
        int i = 0;
        bool flag = false;
        while (true)
        {
            flag = false;

            if (i < playerBulletCount)
            {
                handGunBulletQueue.Enqueue(CreateNewObject(BulletType.HandGun));
                SMGBulletQueue.Enqueue(CreateNewObject(BulletType.SMG));
                flag = true;
            }
            if(i < missileBulletCount)
            {
                missileBulletQueue.Enqueue(CreateNewObject(BulletType.Missile));
                flag = true;
            }
            if (i < bossBulletCount)
            {
                bossMissileBulletQueue.Enqueue(CreateNewObject(BulletType.BossMissile));
                bossRockBulletQueue.Enqueue(CreateNewObject(BulletType.BossRock));
                flag = true;
            }

            i++;
            if (!flag)
                break;
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
     * @date last change 2022/07/28
     */
    private Bullet CreateNewObject(BulletType bulletType)
    {
        Bullet newObj;

        switch (bulletType)
        {
            case BulletType.HandGun:
                newObj = Instantiate(handGunBulletPrefab).GetComponent<Bullet>();
                break;
            case BulletType.SMG:
                newObj = Instantiate(SMGBulletPrefab).GetComponent<Bullet>();
                break;
            case BulletType.Missile:
                newObj = Instantiate(missileBulletPrefab).GetComponent<Bullet>();
                break;
            case BulletType.BossMissile:
                newObj = Instantiate(bossMissileBulletPrefab).GetComponent<Bullet>();
                break;
            case BulletType.BossRock:
                newObj = Instantiate(bossRockBulletPrefab).GetComponent<Bullet>();
                break;
            default:
                Debug.Log("ObjectPool.CreateNewObject() is fail");
                return null;
        }

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
     * @date last change 2022/07/28
     */
    public static Bullet GetObject(BulletType bulletType)
    {

        Queue<Bullet> queue;
        
        switch(bulletType)
        {
            case BulletType.HandGun:
                queue = Instance.handGunBulletQueue;
                break;
            case BulletType.SMG:
                queue = Instance.SMGBulletQueue;
                break;
            case BulletType.Missile:
                queue = Instance.missileBulletQueue;
                break;
            case BulletType.BossMissile:
                queue = Instance.bossMissileBulletQueue;
                break;
            case BulletType.BossRock:
                queue = Instance.bossRockBulletQueue;
                break;
            default:
                Debug.Log("ObjectPool.GetObject() is fail");
                return null;
        }

        if (queue.Count > 0)
        {
            var obj = queue.Dequeue();
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject(bulletType);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
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
     * @date last change 2022/07/28
     */
    public static void ReturnObject(Bullet obj, BulletType bulletType)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);


        switch (bulletType)
        {
            case BulletType.HandGun:
                Instance.handGunBulletQueue.Enqueue(obj);
                break;
            case BulletType.SMG:
                Instance.SMGBulletQueue.Enqueue(obj);
                break;
            case BulletType.Missile:
                Instance.missileBulletQueue.Enqueue(obj);
                break;
            case BulletType.BossMissile:
                Instance.bossMissileBulletQueue.Enqueue(obj);
                break;
            case BulletType.BossRock:
                Instance.bossRockBulletQueue.Enqueue(obj);
                break;
        }
    }

    #endregion



    #region Unity Event

    private void Awake()
    {
        Initialize();
    }

    #endregion
}
