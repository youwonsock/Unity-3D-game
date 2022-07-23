using UnityEngine;

/**
 * @brief Interface InputŬ������ ������ �ذ��� ���� ���� interface
 * @details inputClass�� ������ �־���ϴ� �޼������ �����ص� interface�Դϴ�.
 * 
 * @author yws
 * @date last change 2022/07/16
 */
public interface IInput
{
    public Vector3 GetNormalizedVec();

    public Vector3 GetMouseTrunVec();

    public bool GetRunInput();

    public bool GetJumpInput();

    public bool GetDodgeInput();

    public bool GetInteractInput();

    public bool GetAttackInput();

    public bool GetGrenadeInput();

    public bool GetReloadInput();

    public int GetSwapInput();
}
