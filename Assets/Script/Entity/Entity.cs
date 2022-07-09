using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class 게임내 플레이어, 몬스터등의 생명체의 베이스 클래스
 * @details 게임내 생명체들의 최상위 클래스입니다.
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
