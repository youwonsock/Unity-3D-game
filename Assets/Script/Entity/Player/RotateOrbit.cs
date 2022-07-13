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

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief Player가 현제 소유한 Grenade개수만큼 List에 할당된 Grenade를 활성화 시키는 메서드
     * @details Player의 GetItem 이벤트에 등록하여 사용합니다.
     * 
     * @author yws
     * @data last change 2022/07/10
     */
    private void ActiveOrbitGrenade()
    {
        for(int i = 0; i < player.Grenades; i++)
            list[i].SetActive(true);
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
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromFixedUpdate(RotateOrbitGrenades);
        player.ChangeGrenadesCount -= ActiveOrbitGrenade;
    }

    #endregion

}
