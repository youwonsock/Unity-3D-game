using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Entity를 상속받은 적들의 베이스 클래스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/23
 */
public class Enemy : Entity
{
    #region Fields

    #endregion



    #region Property

    #endregion



    #region Funtion



    #endregion



    #region Unity Event

    protected override void Awake()
    {
        // Entity
        base.Awake();

        bool checkComponent = TryGetComponent<IInput>(out input);
        TryGetComponent<EntityMovement>(out movement);
        TryGetComponent<Rigidbody>(out rigid);
        mat = GetComponent<MeshRenderer>().material;
        //transform.GetChild(0).TryGetComponent<Animator>(out animator); not exist!!!


        OnDeath += () => { gameObject.layer = 8; };
    }

    #endregion
}