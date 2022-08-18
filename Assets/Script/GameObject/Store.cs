using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief Class Store의 RectTransform과 Animation등을 설정해주는 클래스
 * @details Item, Weapon Store Object에 사용되는 상점 기능을 구현한 클래스입니다.
 * 
 * @author yws
 * @date last change 2022/08/19
 */

public enum StoreType { Item = 0, Weapon}

public class Store : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject uiGroup;
    [SerializeField] Animator anim;
    [SerializeField] Transform ItemCreatePosition;

    [SerializeField] GameObject[] itemObj;
    [SerializeField] int[] itemPrice;
    [SerializeField] Text npcText;
    [SerializeField] StoreType storeType;

    Player enterPlayer;

    #endregion



    #region Funtions

    /**
     * @brief 상점 이용 메서드
     * 
     * @author yws
     * @date last change 2022/08/02
     */
    public void SetStoreUi()
    {
        uiGroup.SetActive(!uiGroup.activeSelf);
        anim.SetTrigger("doHello");

        if (enterPlayer != null)
            enterPlayer.CanMove = !uiGroup.activeSelf;
    }

    /**
     * @brief 아이템 구매 메서드
     * 
     * @author yws
     * @date last change 2022/08/19
     */
    public void Buy(int index)
    {
        // 이미 소지한 아이템을 구매하는 경우
        if(storeType == StoreType.Item)
        {
            switch (index)
            {
                case 0:
                    if (enterPlayer.Health == enterPlayer.MaxHealth)
                    {
                        StartCoroutine(ChangeText("이미 최대 체력입니다."));
                        return;
                    }
                    break;
                case 2:
                    if(enterPlayer.Grenades == enterPlayer.MaxGrenades)
                    {
                        StartCoroutine(ChangeText("이미 최대 수류탄 소지량입니다."));
                        return;
                    }
                    break;
            }
        }
        else
        {
            if (enterPlayer.HasWeapon[index] == true)
            {
                StartCoroutine(ChangeText("이미 가지고 있는 무기입니다."));
                return;
            }
        }

        int price = itemPrice[index];
        if(price > enterPlayer.Coin)
        {
            StartCoroutine(ChangeText("금액이 부족합니다."));
            return;
        }

        enterPlayer.Coin -= price;
        Instantiate(itemObj[index], ItemCreatePosition);
    }

    /**
     * @brief npcText 변경 Coroutine
     * 
     * @param[in] changeText : 변경할 택스트
     * 
     * @author yws
     * @date last change 2022/08/19
     */
    IEnumerator ChangeText(string changeText = " ChangeText의 기본 매개변수!")
    {
        npcText.text = changeText;
        yield return new WaitForSecondsRealtime(2f);

        if (storeType == StoreType.Item)
            npcText.text = "아이템 상점";
        else
            npcText.text = "무기 상점";

        yield break;
    }

    #endregion



    #region Unity Event

    private void OnEnable()
    {
        GameManager.Instance.StartStageEvent += () => transform.parent.gameObject.SetActive(false);
        GameManager.Instance.EndStageEvent += () => transform.parent.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        GameManager.Instance.StartStageEvent -= () => transform.parent.gameObject.SetActive(false);
        GameManager.Instance.EndStageEvent -= () => transform.parent.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))  
            other.TryGetComponent<Player>(out enterPlayer);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            if(enterPlayer == null)
                other.TryGetComponent<Player>(out enterPlayer);
            else
            {
                if (enterPlayer.InteractionInput && !uiGroup.activeSelf)
                    SetStoreUi();
                else if(uiGroup.activeSelf)
                    enterPlayer.CanMove = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("doHello");
            uiGroup.SetActive(false);
            enterPlayer.CanMove = true;
            enterPlayer = null;
        }
    }

    #endregion
}
