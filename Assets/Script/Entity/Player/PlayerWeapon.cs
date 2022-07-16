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
    private float castingTime;

    #endregion



    #region Property

    public int CurrentWeaponIdx { get { return currentWeaponIdx; } }

    public float CastingTime { get { return castingTime; } }

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
     * @brief ź�� ���� �޼���
     * @details �ް������� value�� ����ŭ ��� ������ ź���� �������ִ� �޼����Դϴ�.
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
     * @brief ���� �޼���
     * @details Player���� ȣ���Ͽ� ����� ������ Attack()�� �����մϴ�.\n
     * ������ ������ idx �� ��ȯ�Ͽ� player���� animation�� �����ŵ�ϴ�.\n
     * ������ �����Ѱ�� -1�� ��ȯ�մϴ�.
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
     * @brief ������ �޼���
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
