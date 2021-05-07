using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorsScript : MonoBehaviour
{
    public GameObject Sun;

    public GameObject[] Main;

    public float r;
    public int count;

    public float speed = 17.0f;
    public float size = 0.7f;

    private static readonly Random getrandom = new Random();

    public static int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    Vector2 GetXY(float r, float angle)
    {
        float x = r * Mathf.Sin(angle);
        float y = r * Mathf.Cos(angle);

        return new Vector2(x, y);
    }

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject meteor = Instantiate(Main[GetRandomNumber(0, Main.Length)]);

            Vector2 XY = GetXY(r, 2 * Mathf.PI / count * i);

            meteor.transform.localScale = new Vector3(size, size, size);

            meteor.transform.SetParent(Sun.transform);

            meteor.AddComponent<SatelightScript>();

            var satelightScript = meteor.GetComponent<SatelightScript>();
            satelightScript.mainPlanet = Sun;
            satelightScript.speed = speed;
            satelightScript.rotateSpeed = speed * 4;

            meteor.transform.position = new Vector3(XY[0], 0, XY[1]);
        }
    }

    void Update()
    {

    }
}
