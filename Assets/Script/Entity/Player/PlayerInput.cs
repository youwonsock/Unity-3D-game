using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief class �÷��̾� Input�� üũ�ϴ� Ŭ����
 * @details �÷��̾� Input�� ���� ���� ��ȯ�ϴ� Ŭ�����Դϴ�.
 * 
 * @author yws
 * @date last change 2022/07/13
 */
public class PlayerInput : MonoBehaviour ,IInput
{
    #region Fields

    [SerializeField] private Camera followCamera;

    private Ray ray;
    RaycastHit rayHit;
    Vector3 tempVec;

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
     * @brief �̵��ϴ� ������ ���⺤�͸� ��ȯ�ϴ� getter
     * @details �̵�Ű �Է� �� �׿� ���� ���⺤�͸� ��ȯ���ݴϴ�.  
     * 
     * @return Vector
     * 
     * @author yws
     * @date last change 2022/07/18
     */
    Vector3 IInput.GetMouseTrunVec() 
    { 
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out rayHit, 100);
        tempVec = rayHit.point - transform.position;
        tempVec.y = 0;
        return tempVec;
    }

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

    /**
     * @brief ��ȣ�ۿ� Ű �Է¿��θ� ��ȯ�ϴ� getter
     * 
     * @return bool
     * 
     * @author yws
     * @date last change 2022/07/06
     */
    bool IInput.GetInteractInput() { return Input.GetButton("Interation"); }


    /**
     * @brief Attack Ű �Է¿��θ� ��ȯ�ϴ� getter
     * 
     * @return int
     * 
     * @author yws
     * @date last change 2022/07/13
     */
    bool IInput.GetAttackInput() { return Input.GetButton("Fire1"); }


    /**
     * @brief Reload Ű �Է¿��θ� ��ȯ�ϴ� getter
     * 
     * @return bool
     * 
     * @author yws
     * @date last change 2022/07/16
     */
    bool IInput.GetReloadInput() { return Input.GetButtonDown("Reload"); }


    /**
     * @brief Swap Ű �Է¿��θ� ��ȯ�ϴ� getter
     * 
     * @return int
     * 
     * @author yws
     * @date last change 2022/07/06
     */
    int IInput.GetSwapInput() 
    {
        if (Input.GetButtonDown("Swap1"))
            return 0;
        else if (Input.GetButtonDown("Swap2"))
            return 1;
        else if (Input.GetButtonDown("Swap3"))
            return 2;

        return -1;
    }

    #endregion



    #region Funtions


    #endregion



    #region UnityEvent

    private void Awake()
    {
        if (followCamera == null)
            Debug.Log($"Some Component is null : {this.name} .PlayerInput.cs");
    }

    #endregion
}
