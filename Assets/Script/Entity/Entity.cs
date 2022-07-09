using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class ���ӳ� �÷��̾�, ���͵��� ����ü�� ���̽� Ŭ����
 * @details ���ӳ� ����ü���� �ֻ��� Ŭ�����Դϴ�.
 * 
 * @author yws
 * @date last change 2022/07/09
 */
public class Entity : MonoBehaviour
{
    #region Fields

    [Header("Info")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    #endregion



    #region Property

    /**
     * @brief maxHealth getter
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public float MaxHealth() { return maxHealth; }

    /**
     * @brief health getter
     * 
     * @author yws
     * @date last change 2022/07/09
     */
    public float Health() { return health; }

    #endregion



    #region Funtion

    #endregion



    #region Unity Event

    #endregion

}
