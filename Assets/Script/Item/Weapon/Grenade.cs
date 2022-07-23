using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class WeaponŬ������ ����� ���� Grenade Ŭ����
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
     * @brief Rotate���� ȣ��Ǵ� Grenade ��ô �޼���
     * @details �Ű������� throwVec �������� Grenade�� �����ϴ�.\n
     * ������ �ð��� ������ ���� �� ������ϴ�.
     * 
     * @param[in] throwVec : ������ ����
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
     * @brief OnTriggerEnter() ���� �����ų �ൿ�� ������ �޼���
     * @details Item�� OnTriggerEnter()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
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
     * @brief Grenade�� ������ �����ϴ� Coroutine
     * @details expTime��ŭ�� �ð��� ���� ���� Grenade�� �ӵ��� 0���� ���� ��\n
     * ���߽�Ű�� Coroutine�Դϴ�.
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

        // �ǰ� ó��
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
