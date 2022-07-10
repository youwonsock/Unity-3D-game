using UnityEngine;

/**
 * @brief class Player�� Data�� �����ϴ� ScriptableObject
 * 
 * @author yws
 * @date last change 2022/07/10
 */
[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] public int amount;
}

