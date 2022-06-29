using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class 플레이어 Input을 체크하는 클레스
 * @details 플레이어 Input에 따른 값을 반환하는 클레스입니다.
 * 
 * @author yws
 * @date last change 2022/06/28
 */
public class PlayerInput : MonoBehaviour ,IInput
{
    #region Fields

    #endregion



    #region Getter

    /**
     * @brief 이동하는 방향의 방향벡터를 반환하는 getter
     * @details 이동키 입력 시 그에 따른 방향벡터를 반환해줍니다.  
     * 
     * @return Vector
     * 
     * @author yws
     * @date last change 2022/06/28
     */
    Vector3 IInput.GetNormalizedVec(){ return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; }

    /**
     * @brief 달리기 키 입력여부를 반환하는 getter
     * 
     * @return Vector
     * 
     * @author yws
     * @date last change 2022/06/28
     */
    bool IInput.GetRunInput(){ return Input.GetButton("Run"); }

    #endregion



    #region Funtions


    #endregion



    #region UnityEvent

    #endregion
}
