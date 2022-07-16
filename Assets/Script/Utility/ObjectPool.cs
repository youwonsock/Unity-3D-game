using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @brief class Object Pool을 구현한 싱글톤 클래스
 * @details 객체의 Instantiate와 Destroy를 줄여주기 위해서 구현한 Object Pool입니다.\n
 * 다른 객체에서의 접근이 용이하도록 싱글톤으로 구현하였습니다.\n
 * 현재는 총알에 대해서만 Object Pooling을 사용합니다.\n\n
 * 
 * 만약 다른 객체에도 사용하게 되는 경우 현제의 enum을 사용하는 방식이 아니라
 * 리플렉션과 제네릭을 사용하는 방식으로 변경한다면 성능은 조금 떨어지더라도 편의성을 챙길 수 있음
 * 
 * 사용법\n
 * Singleton 객체인 Object Pool 다른 클래스에서 바로 참조가 가능합니다.\n
 * Object Pool에 접근하여 객채를 생성합니다.
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
     * @brief Object Pool 초기화 함수
     * @details Object Pool을 초기화 하는 메서드입니다.\n
     * inspector에서 설정한 총알 수 만큼 각 queue에 총알을 추가합니다.
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
     * @brief bullet prefab 생성 함수
     * @details queue에 저장된 Prefab가 부족한 경우 새로운 객체를 생성해주기 위해 호출하는 메서드입니다.
     * 
     * @return Bullet
     * 
     * @param[in] bulletType : 생성하는 Bullet의 종류
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
     * @brief Object Pool에 저장된 객체를 가져오는 메서드
     * @details 매개변수로 전달된 bulletType에 따라 필요한 총알을 반환하는 메서드 입니다.\n
     * 사용자는 반환 받은 객체 사용 후 ReturnObject를 호출해야합니다.
     * 
     * @return Bullet
     * @param[in] bulletType : Bullet의 종류
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
     * @brief Object Pool에 객체를 반환하는 메서드
     * @details 매개변수로 사용한 객체와 Type을 반환하여 Object Pool에 다시 추가합니다.
     * 
     * @param[in] obj : 반환하는 Object
     * @param[in] bulletType : 반환하는 Bullet의 타입
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
