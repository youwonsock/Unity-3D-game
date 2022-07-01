using System.Collections;
using UnityEngine;

/**
 * 
 */
public interface IInput
{
    public Vector3 GetNormalizedVec();

    public bool GetRunInput();

    public bool GetJumpInput();

    public bool GetDodgeInput();
}
