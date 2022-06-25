using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Fields

    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;

    #endregion

    #region Funtions

    private void UpdateWork()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        //test를 위해 임시로 여기서 이동!
        transform.position += moveVec * speed * Time.deltaTime;
    }

    #endregion

    #region UnityEvent
    void Start()
    {

    }

    private void OnEnable()
    {
        UpdateManager.SubscribeToUpdate(UpdateWork);
    }

    private void OnDisable()
    {
        UpdateManager.UnsubscribeFromUpdate(UpdateWork);
    }

    #endregion
}
