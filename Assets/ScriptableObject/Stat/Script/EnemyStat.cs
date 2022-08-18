using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Enemy의 Data를 저장하는 ScriptableObject
 * 
 * @author yws
 * @date last change 2022/08/19
 */
public enum EnemyType { A = 0, B, C, D}

[CreateAssetMenu(fileName = "EnemyStat", menuName = "Scriptable Object/EnemyStat")]
public class EnemyStat : EntityStat
{
    [SerializeField] private int damage;
    [SerializeField] private int itemCreateProbability;
    [SerializeField] private float attackCooltime;
    [SerializeField] private float attackDistance;
    [SerializeField] private EnemyType type;

    #region Property

    public int Damage { get { return damage; } set { damage = value; } }
    public int ItemCreateProbability { get { return itemCreateProbability; } set { itemCreateProbability = value; } }
    public float AttackCooltime { get { return attackCooltime; } set { attackCooltime = value; } }
    public float AttackDistance { get { return attackDistance; } set { attackDistance = value; } }
    public EnemyType Type { get { return type; } }

    #endregion
}

