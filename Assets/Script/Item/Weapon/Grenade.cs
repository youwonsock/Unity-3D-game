using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class Weapon클래스의 상속을 받은 Grenade 클래스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/09
 */
public class Grenade : Item
{

    #region Fields


    [SerializeField] GrenadeData grenadeData;

    [SerializeField] private GameObject meshObj;
    [SerializeField] private GameObject effectObj;

    private Rigidbody rigid;


    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief Rotate에서 호출되는 Grenade 투척 메서드
     * @details 매개변수인 throwVec 방향으로 Grenade를 던집니다.\n
     * 지정한 시간이 지나면 폭발 후 사라집니다.
     * 
     * @param[in] throwVec : 던지는 방향
     * 
     * @author yws
     * @data last change 2022/07/23
     */
    public void Throw(Vector3 throwVec)
    {
        throwVec.y = 7;
        rigid.AddForce(throwVec, ForceMode.Impulse);
        rigid.AddTorque(Vector3.back * 10, ForceMode.Impulse);
        StartCoroutine(Explosion());
    }

    /**
     * @brief OnTriggerEnter() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerEnter()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/09
     */
    protected override void ActWhenTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isRotate)
        {
            Player player;
            other.TryGetComponent<Player>(out player);

            player.Grenades += 1;

            Destroy(this.gameObject);
        }
    }

    /**
     * @brief Grenade의 폭발을 구현하는 Coroutine
     * @details expTime만큼의 시간이 지난 다음 Grenade의 속도를 0으로 만든 후\n
     * 폭발시키는 Coroutine입니다.
     * 
     * @author yws
     * @data last change 2022/07/23
     */
    IEnumerator Explosion()
    {
        yield return new WaitForSecondsRealtime(grenadeData.Time);
        rigid.velocity = rigid.angularVelocity = Vector3.zero;
        meshObj.SetActive(false);
        effectObj.SetActive(true);

        // 피격 처리
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0);

        IDamageAble damageAble;
        foreach (RaycastHit hit in rayHits)
        {
            hit.transform.TryGetComponent<IDamageAble>(out damageAble);
            if (damageAble != null)
                damageAble.Hit(grenadeData.Damage, Vector3.up, 6);
        }

        Destroy(this.gameObject,3f);
    }

    #endregion



    #region Unity Event

    private void Awake()
    {
        if (!isRotate)
        {
            if (!TryGetComponent<Rigidbody>(out rigid))
                Debug.Log($"Some Component is null : {this.gameObject} .Grenade.cs");
            
            if(!grenadeData || !meshObj || !effectObj)
                Debug.Log($"if isRotate is false, Grenades needs data, mesh, effect obj. \n Object name : "+this.gameObject);
        }
    }
    #endregion

}
