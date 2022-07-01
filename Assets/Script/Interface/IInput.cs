using System.Collections;
using UnityEngine;

/**
 * @brief Interface InputŬ������ ������ �ذ��� ���� ���� interface
 * @details inputClass�� ������ �־���ϴ� �޼������ �����ص� interface�Դϴ�.
 * 
 * @author yws
 * @date last change 2022/07/01
 */
public interface IInput
{
    public Vector3 GetNormalizedVec();

    public bool GetRunInput();

    public bool GetJumpInput();

    public bool GetDodgeInput();
}
