using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Entity 객체들의 이동을 구현한 클레스
 * @details Entity 객체들의 실질적인 이동을 구현한 클래스입니다.\n
 * 
 * @author yws
 * @date last change 2022/06/28
 */
public class EntityMovement : MonoBehaviour
{
    #region Fields

    //Components

    //Values
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    #endregion

    #region Funtion
    /**
     * @brief 객체를 이동시키는 메서드
     * @details 매개변수로 이동방향과 달리기 키 입력여부를 받아 이동방향으로 설정된 속도만큼 이동시킵니다.
     * 
     * @param[in] directionVec : 이동방향
     * @param[in] runInput : 달리기 키 입력여부
     * 
     * @author yws
     * @date last change 2022/06/28
     */
    public void EntityMove(Vector3 directionVec, bool runInput)
    {
        transform.LookAt(transform.position + directionVec);
        transform.position += directionVec * (runInput ? runSpeed : speed)* Time.deltaTime;
    }

    #endregion

    #region Unity Event

    #endregion
}
