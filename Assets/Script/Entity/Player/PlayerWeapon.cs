using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class Player�� ����ϴ� ������� �����ϴ� Ŭ���� 
 * @details Player�� ����ϴ� ������� �迭�� ����
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
     * @brief ���� ���� �޼���(PlayerWeapon)
     * @details Item�� OnTriggerExit()���� �����ų ������ override�� �����ϴ� �޼����Դϴ�.
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
