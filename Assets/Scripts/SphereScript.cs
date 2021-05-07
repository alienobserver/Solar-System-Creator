using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;

    public float speed = 20.0f;

    public float rotateSpeed;

    void Start()
    {
        if (rotateSpeed == 0 ) rotateSpeed = speed * 2 * Time.deltaTime;
    }

    void Update()
    {
        transform.RotateAround( new Vector3( x, y, z), Vector3.up, speed * Time.deltaTime );
        transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }
}
