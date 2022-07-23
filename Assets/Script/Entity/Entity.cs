using System;
using UnityEngine;
using System.Collections;

/**
 * @brief class ���ӳ� �÷��̾�, ���͵��� ����ü�� ���̽� Ŭ����
 * @details ���ӳ� ����ü���� �ֻ��� Ŭ�����Դϴ�.
 * 
 * @author yws
 * @date last change 2022/07/13
 */
public abstract class Entity : MonoBehaviour, IDamageAble
{
    #region Fields

    [Header("Info")]
    [SerializeField] protected EntityStat stat;
    [SerializeField] private float health;
    protected bool isHit;

    //��� Entity���� �������� ������ Component 
    [SerializeField]protected Material mat;
    protected IInput input;
    protected EntityMovement movement;
    protected Animator animator;
    protected Rigidbody rigid;

    #endregion



    #region Property

    /**
     * @brief maxHealth getter
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public float MaxHealth { get { return stat.MaxHealth; } }

    /**
     * @brief health getter
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public float Health 
    { 
        get 
        { 
            return health; 
        } 
        set
        {
            health = Mathf.Clamp(value, -1, stat.MaxHealth);

            if (health < 1)
                if(OnDeath != null)
                    OnDeath();
        }
    }

    #endregion



    #region Funtion

    //--------------------------public--------------------------------------

    /**
     * @brief event Entity ����� �ߵ��Ǵ� �̺�Ʈ
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    public event Action OnDeath;

    /**
     * @brief IDamageAble�� ���� ����
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    bool IDamageAble.Hit(float Damage, Vector3 direction, float knockForce)
    {
        // ���� �ǰ� ó��
        isHit = true;
        Health -= Damage;
        Vector3 reactVec = direction + Vector3.up;
        rigid.AddForce(reactVec * knockForce,ForceMode.Impulse);

        // entity�� �ǰ� ó���� OnDamaged�� override�ؼ� ����
        OnDamaged();

        return true;
    }

    //--------------------------private--------------------------------------

    /**
     * @brief OnDamaged���� ȣ���ϴ� Coroutine
     * 
     * @author yws
     * @date last change 2022/07/23
     */
    IEnumerator OnDamagedCoroutine()
    {
        var wfs = new WaitForSecondsRealtime(0.3f);
        mat.color = Color.red;

        yield return  wfs;

        if (health > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;
            Destroy(gameObject, 4);
        }

        yield return wfs;
        isHit = false;
    }

    //--------------------------protected--------------------------------------

    /**
     * @brief �ǰݽ� �ߵ��Ǵ� �ǰ� ó�� �޼���
     * @details �÷��̾ ���Ͱ� ������ �޾��� ��� ����Ǵ� �޼����Դϴ�.\n
     * ������ �ǰ� ó���� �ʿ��� ��� ���� Ŭ�������� override�Ͽ� �� entity���� �ٸ� �ǰ� ȿ���� ������ �� �ֽ��ϴ�.
     * 
     * @author yws
     * @date last change 2022/07/23
     */
    protected virtual void OnDamaged()
    {
        StartCoroutine(OnDamagedCoroutine());
    }

    #endregion



    #region Unity Event

    protected virtual void Awake()
    {
        health = MaxHealth;
    }

    #endregion

}
