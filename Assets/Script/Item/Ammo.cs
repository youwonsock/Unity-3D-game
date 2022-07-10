using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class 추상 클래스인 Item클래스의 상속을 받은 Ammo 클래스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/09
 */
public class Ammo : Item
{

    #region Fields

    //ScriptableObject
    [SerializeField] private ItemData itemData;

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief OnTriggerEnter() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerEnter()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/09
     */
    protected override void ActWhenTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerWeapon>().FillAmmo(itemData.amount);

            Destroy(this.gameObject);
        }
    }
    #endregion



    #region Unity Event

    #endregion

}
