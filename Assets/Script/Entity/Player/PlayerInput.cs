using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class �÷��̾� Input�� üũ�ϴ� Ŭ����
 * @details �÷��̾� Input�� ���� ���� ��ȯ�ϴ� Ŭ�����Դϴ�.
 * 
 * @author yws
 * @date last change 2022/06/28
 */
public class PlayerInput : MonoBehaviour ,IInput
{
    #region Fields

    #endregion



    #region Getter

    /**
     * @brief �̵��ϴ� ������ ���⺤�͸� ��ȯ�ϴ� getter
     * @details �̵�Ű �Է� �� �׿� ���� ���⺤�͸� ��ȯ���ݴϴ�.  
     * 
     * @return Vector
     * 
     * @author yws
     * @date last change 2022/06/28
     */
    Vector3 IInput.GetNormalizedVec(){ return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; }

    /**
     * @brief �޸��� Ű �Է¿��θ� ��ȯ�ϴ� getter
     * 
     * @return bool
     * 
     * @author yws
     * @date last change 2022/06/28
     */
    bool IInput.GetRunInput(){ return Input.GetButton("Run"); }


    /**
     * @brief ���� Ű �Է¿��θ� ��ȯ�ϴ� getter
     * 
     * @return bool
     * 
     * @author yws
     * @date last change 2022/06/30
     */
    bool IInput.GetJumpInput(){ return Input.GetButtonDown("Jump"); }

    /**
     * @brief ȸ�� Ű �Է¿��θ� ��ȯ�ϴ� getter
     * 
     * @return bool
     * 
     * @author yws
     * @date last change 2022/07/01
     */
    bool IInput.GetDodgeInput() { return Input.GetKeyDown(KeyCode.C); }

    #endregion



    #region Funtions


    #endregion



    #region UnityEvent

    #endregion
}
