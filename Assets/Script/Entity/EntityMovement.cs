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
public class EntityMovement : MonoBehaviour
{
    #region Fields

    //Components

    //Values
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    #endregion

    #region Funtion
    /**
     * @brief 
     * @details 
     * 
     * @author yws
     * @data last change 2022/06/28
     */
    public void EntityMove(Vector3 directionVec, bool runInput)
    {
        transform.LookAt(transform.position + directionVec);
        transform.position += directionVec * (runInput ? runSpeed : speed)* Time.deltaTime;
    }

    #endregion

    #region Unity Event

    #endregion
}
