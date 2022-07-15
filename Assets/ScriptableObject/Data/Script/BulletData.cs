using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Bullet Data를 저장하는 ScriptableObject
 * 
 * @author yws
 * @date last change 2022/07/16
 */
[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Object/BulletData")]
public class BulletData : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float time;
    [SerializeField] private float stiffen;

    #region Property

    public float Damage { get { return damage; } }

    public float Time { get { return time; } }

    public float Stiffen { get { return stiffen; } }

    #endregion
}
