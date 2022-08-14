using UnityEngine;

/**
 * @brief class Item Drop�� ���� �޼��带 ������ Ŭ����
 * 
 * @author yws
 * @date last change 2022/08/13
 */
public class ItemManager : MonoBehaviour
{
    #region Funtion



    #endregion



    #region Unity Event

    /**
     * @brief item Drop �޼���
     * @details �Ű������� ����Ȯ���� 
     * 
     * @param[in] probability : ������ ����Ȯ��
     * 
     * @author yws
     * @date last change 2022/08/02
     */
    public static void DropItem(int probability, Transform enemyTransform)
    {
        if(Random.Range(0,100) < probability)
        {
            int n = Random.Range(0,100);

            if (n < 30)
                Instantiate(GameManager.Instance.Items[0],enemyTransform.position, Quaternion.identity);
            else if (n < 60)
                Instantiate(GameManager.Instance.Items[1], enemyTransform.position, Quaternion.identity);
            else if (n < 80)
                Instantiate(GameManager.Instance.Items[2], enemyTransform.position, Quaternion.identity);
            else if (n < 90)
                Instantiate(GameManager.Instance.Items[3], enemyTransform.position, Quaternion.identity);
            else
                Instantiate(GameManager.Instance.Items[4], enemyTransform.position, Quaternion.identity);
        }
    }

    #endregion

}
