using UnityEngine;

/**
 * @brief class Grenade Data를 저장하는 ScriptableObject
 * 
 * @author yws
 * @date last change 2022/07/23
 */
[CreateAssetMenu(fileName = "GrenadeData", menuName = "Scriptable Object/GrenadeData")]
public class GrenadeData : ScriptableObject
{
    #region Fields

    [SerializeField] private float damage;
    [SerializeField] private float time;

    #endregion



    #region Property

    public float Damage { get { return damage; } set { damage = value; } }

    public float Time { get { return time; } set { time = value; } }

    #endregion
}
