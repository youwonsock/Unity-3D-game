using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class Player�� ����ϴ� ������� �����ϴ� Ŭ���� 
 * @details Player�� ����ϴ� ������� �迭�� ����
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

    #endregion



    #region Property

    public int CurrentWeaponIdx { get { return currentWeaponIdx; } }

    #endregion



    #region Funtion

    /**
     * @brief ���� ���� �޼���(PlayerWeapon)
     * @details Player���� ȣ��Ǿ� ���⸦ �����ϴ� �޼���
     * hasWeapon[swapIdx]�� false�̰ų� �ٲٷ��� ������ idx�� ���� ����� ���ٸ� �Ͽ� �޼��带 �����մϴ�.
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
     * @brief ���� ȹ�� �޼���
     * @details Item���� ȣ��Ǿ� ȹ���� ���⸦ Ȱ��ȭ ��Ű�� �޼���
     * @return bool
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    public bool GetWeapon(int itemValue, bool interactionInput)
    {
        if (!interactionInput)
            return false;

        for(int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].Value == itemValue && !hasWeapon[i])
            {
                if (hasWeapon[i])
                    continue;

                weapons[currentWeaponIdx].gameObject.SetActive(false);
                weapons[i].gameObject.SetActive(true);

                hasWeapon[i] = true;
                currentWeaponIdx = i;

                return true;
            }
        }

        return false;
    }



    #endregion



    #region Unity Event


    #endregion
}
