using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class Entity ��ü���� �̵��� ������ Ŭ����
 * @details Entity ��ü���� �������� �̵��� ������ Ŭ�����Դϴ�.\n
 * 
 * @author yws
 * @date last change 2022/06/28
 */
public class EntityMovement : MonoBehaviour
{
    #region Fields

    //Components

    //Values
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    #endregion

    #region Funtion
    /**
     * @brief ��ü�� �̵���Ű�� �޼���
     * @details �Ű������� �̵������ �޸��� Ű �Է¿��θ� �޾� �̵��������� ������ �ӵ���ŭ �̵���ŵ�ϴ�.
     * 
     * @param[in] directionVec : �̵�����
     * @param[in] runInput : �޸��� Ű �Է¿���
     * 
     * @author yws
     * @date last change 2022/06/28
     */
    public void EntityMove(Vector3 directionVec, bool runInput)
    {
        transform.LookAt(transform.position + directionVec);
        transform.position += directionVec * (runInput ? runSpeed : speed)* Time.deltaTime;
    }

    #endregion

    #region Unity Event

    #endregion
}
