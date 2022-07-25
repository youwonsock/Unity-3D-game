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
    [SerializeField] private float speed;
    [SerializeField] private ObjectPool.BulletType bulletType;
    [SerializeField] private Constants.Shooter shooter;

    #region Property

    public float Damage { get { return damage; } set { damage = value; } }

    public float Time { get { return time; } set { time = value; } }

    public float Speed { get { return speed; } set { speed = value; } }

    public Constants.Shooter Shooter { get { return shooter; } set { shooter = value; } }

    public ObjectPool.BulletType BulletType { get { return bulletType; }  set { bulletType = value; } }

    #endregion
}
