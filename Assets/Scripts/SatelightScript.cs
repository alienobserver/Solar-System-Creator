using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelightScript : MonoBehaviour
{
    public GameObject mainPlanet;

    public float speed = 20.0f;

    public float rotateSpeed;

    void Start()
    {
        if(rotateSpeed == 0 ) rotateSpeed = speed * 4 * Time.deltaTime;
    }

    void Update()
    {
        transform.RotateAround(mainPlanet.transform.position, Vector3.up, speed * Time.deltaTime);
        transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }
}
