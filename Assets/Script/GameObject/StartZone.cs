using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class 총알의 기능을 구현한 클레스
 * @details 
 * 
 * @author yws
 * @date last change 2022/07/28
 */
public class StartZone : MonoBehaviour
{
    #region Unity Event

    private void OnEnable()
    {
        GameManager.Instance.StartStageEvent += () => this.gameObject.SetActive(false);
        GameManager.Instance.EndStageEvent += () => this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.StartStage();
        }
    }

    #endregion
}
