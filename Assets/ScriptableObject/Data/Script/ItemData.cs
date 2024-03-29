using UnityEngine;

/**
 * @brief class Item Data를 저장하는 ScriptableObject
 * 
 * @author yws
 * @date last change 2022/07/10
 */
[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private int amount;


    #region Property

    public int Amount { get { return amount; } }

    #endregion
}

