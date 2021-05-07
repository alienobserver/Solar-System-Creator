using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelightController : MonoBehaviour
{
    public GameObject mainPlanet;

    public GameObject[] planets;

    public int count;
    public float r;
    public float more;
    public float rotateSpeed;
    public float speed;
    public float size;

    private static readonly Random getrandom = new Random();

    public static int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            float satelightSize = size / 2;
            r += more;
            speed += 5;

            GameObject satelight = Instantiate(planets[GetRandomNumber(0, planets.Length)]);

            satelight.transform.localScale = new Vector3( satelightSize, satelightSize, satelightSize );

            satelight.transform.SetParent(mainPlanet.transform);

            satelight.transform.position = new Vector3(r, 0, 0);

            satelight.AddComponent<SatelightScript>();

            var satelightScript = satelight.GetComponent<SatelightScript>();
            satelightScript.speed = speed / Time.deltaTime;
            satelightScript.rotateSpeed = rotateSpeed;
            satelightScript.mainPlanet = mainPlanet;
        }
    }


    void Update()
    {

    }
}
