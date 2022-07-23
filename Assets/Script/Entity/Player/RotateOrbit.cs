using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @brief Class Player의 필살기인 Grenade를 회전시키는 클레스
 * 
 * @author yws
 * @date last change 2022/07/10
 */
public class RotateOrbit : MonoBehaviour
{

    #region Fields

    [SerializeField] private Player player;
    [SerializeField] private int speed;
    [SerializeField] private List<GameObject> list;
    [SerializeField] private GameObject throwGrenadeObj;

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief Player가 현제 소유한 Grenade개수만큼 List에 할당된 Grenade를 활성화 시키는 메서드
     * @details Player의 ChangeGrenadesCount 이벤트에 등록하여 사용합니다.
     * 
     * @author yws
     * @data last change 2022/07/10
     */
    private void ActiveOrbitGrenade()
    {
        int i = 0;
        while (i < list.Count)
        {
            if(i < player.Grenades)
                list[i].SetActive(true);
            else
                list[i].SetActive(false);

            i++;
        }

    }

    /**
     * @brief Player의 필살기인 Grenade를 사용하는 메서드
     * @details Player의 UseGrenade 이벤트에 등록하여 player의 grenadesInput이 true인 경우 실행됩니다.
     * 
     * @param[in] throwVec : 던지는 방향
     * 
     * @author yws
     * @data last change 2022/07/23
     */
    private void ThrowGrenade(Vector3 throwVec)
    {
        if (player.Grenades > 0)
        {
            player.Grenades--;
            Grenade throwGrenade;
            GameObject instantGrenade = Instantiate(throwGrenadeObj, transform.position, transform.rotation);
            instantGrenade.TryGetComponent<Grenade>(out throwGrenade);
            throwGrenade.Throw(throwVec);
        }
    }

    /**
     * @brief Grenade Group을 회전시켜주는 메서드
     * 
     * @author yws
     * @data last change 2022/07/10
     */
    private void RotateOrbitGrenades()
    {
        transform.Rotate(Vector3.up * speed);
    }

    #endregion



    #region Unity Event

    private void OnEnable()
    {
        UpdateManager.SubscribeToFixedUpdate(RotateOrbitGrenades);
        player.ChangeGrenadesCount += ActiveOrbitGrenade;
        player.UseGrenade += ThrowGrenade;
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromFixedUpdate(RotateOrbitGrenades);
        player.ChangeGrenadesCount -= ActiveOrbitGrenade;
        player.UseGrenade -= ThrowGrenade;
    }

    #endregion

}
