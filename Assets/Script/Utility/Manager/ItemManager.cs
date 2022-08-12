using UnityEngine;

/**
 * @brief class Item Drop을 위한 메서드를 정의한 클래스
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
     * @brief item Drop 메서드
     * @details 매개변수로 생성확률을 
     * 
     * @param[in] probability : 아이템 생성확률
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
