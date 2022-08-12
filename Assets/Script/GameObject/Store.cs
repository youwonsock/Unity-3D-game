using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief Class Store의 RectTransform과 Animation등을 설정해주는 클래스
 * @details Item, Weapon Store Object에 사용되는 상점 기능을 구현한 클래스입니다.
 * 
 * @author yws
 * @date last change 2022/08/02
 */
public class Store : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject uiGroup;
    [SerializeField] Animator anim;
    [SerializeField] Transform ItemCreatePosition;

    [SerializeField] GameObject[] itemObj;
    [SerializeField] int[] itemPrice;
    [SerializeField] Text npcText;

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
     * @date last change 2022/08/02
     */
    public void Buy(int index)
    {
        int price = itemPrice[index];
        if(price > enterPlayer.Coin)
        {
            StartCoroutine(ChangeText());
            return;
        }

        enterPlayer.Coin -= price;
        Instantiate(itemObj[index], ItemCreatePosition);
    }

    /**
     * @brief npcText 변경 Coroutine
     * 
     * @author yws
     * @date last change 2022/08/02
     */
    IEnumerator ChangeText()
    {
        string tempText = npcText.text;
        npcText.text = "금액이 부족합니다!";
        yield return new WaitForSecondsRealtime(2f);
        npcText.text = tempText;

        yield break;
    }

    #endregion



    #region Unity Event

    private void OnEnable()
    {
        GameManager.Instance.StartStageEvent += () => transform.parent.gameObject.SetActive(false);
        GameManager.Instance.EndStageEvent += () => transform.parent.gameObject.SetActive(true);
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
                if(enterPlayer.InteractionInput && !uiGroup.activeSelf)
                    SetStoreUi();
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
