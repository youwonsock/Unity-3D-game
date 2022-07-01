using System.Collections;
using UnityEngine;

/**
 * @brief Interface Input클레스에 의존성 해결을 위해 만든 interface
 * @details inputClass가 가지고 있어야하는 메서드들을 정의해둔 interface입니다.
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
