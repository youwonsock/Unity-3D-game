using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class Player가 사용하는 무기들을 관리하는 클래스 
 * @details Player가 사용하는 무기들을 배열로 가지
 * 
 * @author yws
 * @date last change 2022/07/07
 */
public class PlayerWeapon : MonoBehaviour
{
    #region Fields

    //SerializeField
    [Header ("Weapon and has Weapon Check")]
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private bool[] hasWeapon;

    private int currentWeaponIdx = 0;
    private float castingTime;

    #endregion



    #region Property

    public int CurrentWeaponIdx { get { return currentWeaponIdx; } }

    public float CastingTime { get { return castingTime; } }

    #endregion



    #region Funtion

    /**
     * @brief 무기 변경 메서드(PlayerWeapon)
     * @details Player에서 호출되어 무기를 변경하는 메서드
     * hasWeapon[swapIdx]가 false이거나 바꾸려는 무기의 idx가 현재 무기와 같다면 하여 메서드를 종료합니다.
     * return bool
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    public bool Swap(int swapIdx)
    {
        if (!hasWeapon[swapIdx] || swapIdx == currentWeaponIdx)
            return false;

        weapons[currentWeaponIdx].gameObject.SetActive(false);
        currentWeaponIdx = swapIdx;

        weapons[currentWeaponIdx].gameObject.SetActive(true);

        return true;
    }

    /**
     * @brief 무기 획득 메서드
     * @details Item에서 호출되어 획득한 무기를 활성화 시키는 메서드
     * @return bool
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    public bool GetWeapon(Weapon.WeaponType itemValue, bool interactionInput)
    {
        if (!interactionInput)
            return false;

        for(int i = 0; i < weapons.Length; i++)
        {
            if (hasWeapon[i])
                continue;
            if (weapons[i].weaponType == itemValue && !hasWeapon[i])
            {
                weapons[currentWeaponIdx].gameObject.SetActive(false);
                weapons[i].gameObject.SetActive(true);

                hasWeapon[i] = true;
                currentWeaponIdx = i;

                return true;
            }
        }

        return false;
    }

    /**
     * @brief 탄약 보급 메서드
     * @details 메개변수인 value의 값만큼 모든 무기의 탄약을 보충해주는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    public void FillAmmo(int value)
    {
        for(int i = 0; i < weapons.Length; i++)
            weapons[i].Ammo += value;
    }

    /**
     * @brief 공격 메서드
     * @details Player에서 호출하여 장비한 무기의 Attack()을 실행합니다.\n
     * 공격한 무기의 idx 를 반환하여 player에서 animation을 실행시킵니다.\n
     * 공격이 실패한경우 -1을 반환합니다.
     * 
     * @return int
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    public int Attack()
    {
        if (!hasWeapon[currentWeaponIdx])
            return -1;

        if (weapons[currentWeaponIdx].FireReady)
        {
            castingTime = weapons[currentWeaponIdx].Attack();
            return currentWeaponIdx;
        }

        return -1;
    }

    /**
     * @brief 재장전 메서드
     * @details 
     * 
     * @return bool
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    public bool Reload()
    {
        return weapons[currentWeaponIdx].Reload();
    }

    #endregion



    #region Unity Event


    #endregion
}
