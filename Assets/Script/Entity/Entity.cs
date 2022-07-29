using System;
using UnityEngine;
using System.Collections;

/**
 * @brief class 게임내 플레이어, 몬스터등의 생명체의 베이스 클래스
 * @details 게임내 생명체들의 최상위 클래스입니다.
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

    //모든 Entity들이 공통으로 가지는 Component 
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
     * @brief event Entity 사망시 발동되는 이벤트
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    public event Action OnDeath;

    /**
     * @brief IDamageAble의 설명 참조
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    bool IDamageAble.Hit(float Damage, Vector3 direction, float knockForce)
    {
        // 공통 피격 처리
        if (isHit)
            return false;

        Health -= Damage;
        if (knockForce > 0)
        {
            Vector3 reactVec = direction + Vector3.up;
            rigid.AddForce(reactVec * knockForce, ForceMode.Impulse);
        }
        // entity별 피격 처리는 OnDamaged를 override해서 구현
        OnDamaged();

        return true;
    }

    //--------------------------private--------------------------------------


    //--------------------------protected--------------------------------------

    /**
     * @brief 피격시 발동되는 피격 처리 메서드
     * @details 플레이어나 몬스터가 공격을 받았을 경우 실행되는 메서드입니다.\n
     * 별도의 피격 처리가 필요한 경우 하위 클래스에서 override하여 각 entity마다 다른 피격 효과를 구현할 수 있습니다.
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
