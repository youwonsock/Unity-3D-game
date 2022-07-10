using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class ���ӳ� �÷��̾�, ���͵��� ����ü�� ���̽� Ŭ����
 * @details ���ӳ� ����ü���� �ֻ��� Ŭ�����Դϴ�.
 * 
 * @author yws
 * @date last change 2022/07/10
 */
public class Entity : MonoBehaviour
{
    #region Fields

    [Header("Info")]
    [SerializeField] protected EntityStat stat;
    [SerializeField] private float health;

    //��� Entity���� �������� ������ Component 
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
            if (health + value > stat.MaxHealth)
                health = stat.MaxHealth;
            else
                health += value;
        }
    }

    #endregion



    #region Funtion

    #endregion



    #region Unity Event

    #endregion

}
