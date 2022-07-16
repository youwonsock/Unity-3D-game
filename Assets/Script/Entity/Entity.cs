using System;
using UnityEngine;

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

    //모든 Entity들이 공통으로 가지는 Component 
    protected IInput input;
    protected EntityMovement movement;
    protected Animator animator;

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
            if (health < 0)
                if(OnDeath != null)
                    OnDeath();

            health = Mathf.Clamp(value, -1, stat.MaxHealth);
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
    bool IDamageAble.Hit(float Damage, Vector3 direction)
    {
        Health -= Damage;

        // 경직 처리

        // 피격 방향 처리

        return true;
    }

    //--------------------------private--------------------------------------

    #endregion



    #region Unity Event

    private void Awake()
    {
        health = MaxHealth;
    }

    #endregion

}
