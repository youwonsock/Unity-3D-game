using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class Weapon클래스의 상속을 받은 Hammer 클래스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/13
 */
public class Hammer : Weapon
{

    #region Fields

    [Header("Hammer")]
    [SerializeField] private float damage;
    [SerializeField] private float rate;
    [SerializeField] private BoxCollider attackArea;
    [SerializeField] private TrailRenderer trailRenderer;


    #endregion



    #region Property

    #endregion



    #region Funtion

    #endregion



    #region Unity Event

    private void Awake()
    {
        Rigidbody rb;

        TryGetComponent<BoxCollider>(out attackArea);
        TryGetComponent<Rigidbody>(out rb);

        if (!attackArea || !trailRenderer)
            Debug.Log($"GetComponent failed : {this.name} .Hammer.cs");
    }

    #endregion

}
