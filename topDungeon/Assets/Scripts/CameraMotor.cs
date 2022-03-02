using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        var deltaX = lookAt.position.x - transform.position.x;
        var deltaY = lookAt.position.y - transform.position.y;

        if (deltaX > boundX || -deltaX > boundX)
        {
            if (lookAt.position.x > transform.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = boundX + deltaX;
            }
        }

        if (deltaY > boundY || -deltaY > boundY)
        {
            if (lookAt.position.y > transform.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = boundY + deltaY;
            }
        }

        transform.position += delta;
    }
}
