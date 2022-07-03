using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class 
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/03
 */
public abstract class Item : MonoBehaviour
{
    #region Fields
    //public 
    public enum ItemType { Ammo, Coin, Grenade, Heart, Weapon };

    //private
    [SerializeField]private ItemType type;
    [SerializeField]private int value;

    #endregion



    #region Property

    #endregion


    #region Funtion

    #endregion



    #region Unity Event

    #endregion

}
