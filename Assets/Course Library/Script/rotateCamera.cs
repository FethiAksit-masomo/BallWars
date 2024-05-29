using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCamera : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.Z))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            horizontalInput = 1f;
        }
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
