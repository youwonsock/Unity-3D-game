using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class 추상 클래스인 Item클래스의 상속을 받은 Weapon 클래스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/07
 */
public class Weapon : Item
{
    #region Fields

    #endregion



    #region Property

    #endregion



    #region Funtion

    /**
     * @brief OnTriggerStay() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerStay()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected override void ActWhenTriggerStay()
    {

    }

    /**
     * @brief OnTriggerStay() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerExit()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/07
     */
    protected override void ActWhenTriggerExit()
    {

    }
    #endregion



    #region Unity Event

    #endregion
}
