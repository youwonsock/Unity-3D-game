using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Class WeaponŬ������ ����� ���� Hammer Ŭ����
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
     * @brief Hammer ���� �޼���
     * @details Hammer.cs���� �������� Weapon�� Attack()�Դϴ�
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
     * @brief �� ������� ���� �޼���
     * @details �ڽ� Ŭ�������� �������Ͽ� �� ������ ������ �����ϴ� �޼����Դϴ�.
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
            Debug.Log($"Some Component is null : {this.name} .Hammer.cs");
    }

    #endregion

}
