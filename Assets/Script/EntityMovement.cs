using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    #region Fields

    [SerializeField] private float speed;

    #endregion

    #region Funtion
    public Vector3 EntityMoveDistance(Vector3 directionVec)
    {
        return directionVec * speed;
    }

    #endregion

}
