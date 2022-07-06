using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class Player가 사용하는 무기들을 관리하는 클래스 
 * @details Player가 사용하는 무기들을 배열로 가지
 * 
 * @author yws
 * @date last change 2022/06/28
 */
public class PlayerWeapon : MonoBehaviour
{
    #region Fields
    [Header ("Weapon and has Weapon Check")]
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private bool[] hasWeapon;

    private int currentWeaponIdx;

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief 무기 변경 메서드(PlayerWeapon)
     * @details Item의 OnTriggerExit()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    public void Swap(int swapIdx)
    {
        switch(swapIdx)
        {
            case 1:
                // 
                break;
            case 2:
                //
                break;
            case 3:
                //
                break;
        }
    }

    #endregion



    #region Unity Event


    #endregion
}
