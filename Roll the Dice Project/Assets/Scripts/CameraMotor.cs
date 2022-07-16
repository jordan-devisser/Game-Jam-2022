using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] Transform cameraTarget;

    [SerializeField] float boundX = 0.15f;
    [SerializeField] float boundY = 0.05f;

    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = cameraTarget.position.x - transform.position.x;

        if (deltaX > boundX || deltaX < -boundX)
        {
            // if target is to the right of camera
            if (transform.position.x < cameraTarget.position.x)
            {
                // shrink the move to the right by boundX
                delta.x = deltaX - boundX;
            }
            else
            {
                // shrink move to left by boundX
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = cameraTarget.position.y - transform.position.y;

        if (deltaY > boundY || deltaY < -boundY)
        {
            // if target is to the right of camera
            if (transform.position.y < cameraTarget.position.y)
            {
                // shrink the move to the right by boundX
                delta.y = deltaY - boundY;
            }
            else
            {
                // shrink move to left by boundX
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
