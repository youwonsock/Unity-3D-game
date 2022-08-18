using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief Class Store�� RectTransform�� Animation���� �������ִ� Ŭ����
 * @details Item, Weapon Store Object�� ���Ǵ� ���� ����� ������ Ŭ�����Դϴ�.
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
     * @date last change 2022/08/19
     */
    public void Buy(int index)
    {
        // �̹� ������ �������� �����ϴ� ���
        if(storeType == StoreType.Item)
        {
            switch (index)
            {
                case 0:
                    if (enterPlayer.Health == enterPlayer.MaxHealth)
                    {
                        StartCoroutine(ChangeText("�̹� �ִ� ü���Դϴ�."));
                        return;
                    }
                    break;
                case 2:
                    if(enterPlayer.Grenades == enterPlayer.MaxGrenades)
                    {
                        StartCoroutine(ChangeText("�̹� �ִ� ����ź �������Դϴ�."));
                        return;
                    }
                    break;
            }
        }
        else
        {
            if (enterPlayer.HasWeapon[index] == true)
            {
                StartCoroutine(ChangeText("�̹� ������ �ִ� �����Դϴ�."));
                return;
            }
        }

        int price = itemPrice[index];
        if(price > enterPlayer.Coin)
        {
            StartCoroutine(ChangeText("�ݾ��� �����մϴ�."));
            return;
        }

        enterPlayer.Coin -= price;
        Instantiate(itemObj[index], ItemCreatePosition);
    }

    /**
     * @brief npcText ���� Coroutine
     * 
     * @param[in] changeText : ������ �ý�Ʈ
     * 
     * @author yws
     * @date last change 2022/08/19
     */
    IEnumerator ChangeText(string changeText = " ChangeText�� �⺻ �Ű�����!")
    {
        npcText.text = changeText;
        yield return new WaitForSecondsRealtime(2f);

        if (storeType == StoreType.Item)
            npcText.text = "������ ����";
        else
            npcText.text = "���� ����";

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
