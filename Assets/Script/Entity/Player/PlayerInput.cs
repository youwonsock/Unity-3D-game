using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief 
 * @details 
 * 
 * @author yws
 * @data last change 2022/06/28
 */
public class PlayerInput : MonoBehaviour
{
    #region Fields

    #endregion



    #region Getter

    /**
     * @brief 
     * @details 
     * 
     * @author yws
     * @data last change 2022/06/28
     */
    public Vector3 GetNormalizedVec() { return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; }

    /**
     * @brief 
     * @details 
     * 
     * @author yws
     * @data last change 2022/06/28
     */
    public bool GetRunInput() { return Input.GetButton("Run"); }

    #endregion



    #region Funtions


    #endregion



    #region UnityEvent

    #endregion
}
