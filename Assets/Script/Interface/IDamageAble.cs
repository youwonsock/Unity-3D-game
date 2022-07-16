using System.Collections;
using UnityEngine;

/**
 * @brief DamageSystem의 단순화를 위해 구현한 interface
 * @details IDamageAble을 상속받은 클래스에서 피격시 처리를 구현하여 \n
 * 공격하는 객체는 IDamageAble을 가지고 있는지만 알고 있으면 공격이 가능합니다.
 * 
 * @author yws
 * @date last change 2022/07/01
 */
public interface IDamageAble
{
    /**
     * @brief DamageSystem의 단순화를 위해 구현한 interface
     * @details IDamageAble을 상속받은 클래스에서 피격시 처리를 구현하여 \n
     * 공격하는 객체는 IDamageAble을 가지고 있는지만 알고 있으면 공격이 가능합니다.
     * 
     * @param[in] Damage : 피해량
     * @param[in] stiffen : 경직도
     * @param[in] direction : 피격방향
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    public bool Hit(float Damage, Vector3 direction);
}
