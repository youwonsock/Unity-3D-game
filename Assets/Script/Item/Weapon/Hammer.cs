using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class Weapon클래스의 상속을 받은 Hammer 클래스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/13
 */
public class Hammer : Weapon
{

    #region Fields

    [Header("Hammer")]
    [SerializeField] private TrailRenderer trailRenderer;
    private BoxCollider attackArea;


    #endregion



    #region Property

    #endregion



    #region Funtion

    //--------------------------public-------------------------------------
    /**
     * @brief Hammer 공격 메서드
     * @details Hammer.cs에서 재정의한 Weapon의 Attack()입니다
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    public override float Attack()
    {
        fireReady = false;
        StartCoroutine(Swing());

        return rate;
    }

    //--------------------------private-------------------------------------

    /**
     * @brief Hammer의 공격의 딜레이를 구현하기 위한 코루틴
     * @details Hammer의 fireReady와 attackArea, trailRenderer등의 공격효과의 시간을 조절하기 위한 코루틴입니다.
     * 
     * @author yws
     * @data last change 2022/07/13
     */
    IEnumerator Swing()
    {
        var wfs = new WaitForSecondsRealtime(rate - 0.1f);

        yield return new WaitForSecondsRealtime(0.1f);
        attackArea.enabled = true;
        trailRenderer.enabled = true;

        yield return wfs;
        attackArea.enabled = false;

        yield return wfs;
        trailRenderer.enabled = false;
        fireReady = true;

        yield break;
    }

    #endregion



    #region Unity Event

    private void Awake()
    {
        TryGetComponent<BoxCollider>(out attackArea);

        if (!attackArea || !trailRenderer)
            Debug.Log($"Some Component is null : {this.gameObject} .Hammer.cs");
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        fireReady = true;
    }

    /**
     * @brief OnTriggerStay() 에서 실행시킬 행동을 정의한 메서드
     * @details Item의 OnTriggerStay()에서 실행시킬 동작을 override로 정의하는 메서드입니다.
     * 
     * @author yws
     * @data last change 2022/07/24
     */
    protected override void ActWhenTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        IDamageAble damageAble;
        other.gameObject.TryGetComponent<IDamageAble>(out damageAble);

        if (damageAble != null)
            damageAble.Hit(damage, transform.forward, 8);
    }

    #endregion

}
