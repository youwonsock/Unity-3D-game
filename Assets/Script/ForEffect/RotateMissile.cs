using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMissile : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.right * 150 * Time.deltaTime);
    }
}
