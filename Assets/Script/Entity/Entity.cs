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
    protected bool isHit = false;
    protected bool isDead = false;

    //��� Entity���� �������� ������ Component 
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

            if (!isDead && health < 1)
            {
                isDead = true;
                StopAllCoroutines();

                if (OnDeath != null)
                    OnDeath();

                Destroy(gameObject, 4);
            }
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
        if (isHit)
            return false;

        Health -= Damage;
        if (knockForce > 0)
        {
            Vector3 reactVec = direction + Vector3.up;
            rigid.AddForce(reactVec * knockForce, ForceMode.Impulse);
        }
        // entity�� �ǰ� ó���� OnDamaged�� override�ؼ� ����
        OnDamaged();

        return true;
    }

    //--------------------------private--------------------------------------


    //--------------------------protected--------------------------------------

    /**
     * @brief �ǰݽ� �ߵ��Ǵ� �ǰ� ó�� �޼���
     * @details �÷��̾ ���Ͱ� ������ �޾��� ��� ����Ǵ� �޼����Դϴ�.\n
     * ������ �ǰ� ó���� �ʿ��� ��� ���� Ŭ�������� override�Ͽ� �� entity���� �ٸ� �ǰ� ȿ���� ������ �� �ֽ��ϴ�.
     * 
     * @author yws
     * @date last change 2022/07/23
     */
    protected virtual void OnDamaged() {}

    #endregion



    #region Unity Event

    protected virtual void Awake()
    {
        health = MaxHealth;
    }

    #endregion

}
