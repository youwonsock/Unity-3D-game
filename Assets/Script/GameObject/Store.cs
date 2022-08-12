using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief Class Store�� RectTransform�� Animation���� �������ִ� Ŭ����
 * @details Item, Weapon Store Object�� ���Ǵ� ���� ����� ������ Ŭ�����Դϴ�.
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
     * @brief ���� �̿� �޼���
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
     * @brief ������ ���� �޼���
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
     * @brief npcText ���� Coroutine
     * 
     * @author yws
     * @date last change 2022/08/02
     */
    IEnumerator ChangeText()
    {
        string tempText = npcText.text;
        npcText.text = "�ݾ��� �����մϴ�!";
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
