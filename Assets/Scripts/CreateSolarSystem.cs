using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSolarSystem : MonoBehaviour
{
    public GameObject[] Planets;

    public float size = 2.5f;
    public float speed;

    public int countPlanets;
    public int countMeteors;

    public float rotation;

    private static readonly UnityEngine.Random getrandom = new UnityEngine.Random();

    public string InputSize = "";
    public string InputSpeed = "";
    public string InputCountPlanets = "";
    public string InputCountMeteors = "";
    public string InputRotation = "";


    public static int GetRandomNumber(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public GameObject createMain(GameObject[] Planets,float size)
    {
        GameObject Sun = Instantiate(Planets[0]);

        Sun.transform.localScale = new Vector3(size * 3, size * 3, size * 3);

        Sun.name = "Main";

        Sun.AddComponent<SphereScript>();

        Quaternion target = Quaternion.Euler(0, 0, 0);
        Sun.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 0);
        Sun.transform.Rotate(new Vector3(rotation, 0, 0));

        return Sun;
    }

    public GameObject createPlanet(GameObject[] Planets,float planetSize,float r, GameObject Sun)
    {
        GameObject planet = Instantiate(Planets[GetRandomNumber(0, Planets.Length)]);

        planet.transform.localScale = new Vector3(planetSize, planetSize, planetSize);
        planet.transform.SetParent(Sun.transform);

        planet.AddComponent<SatelightScript>();

        var satelightScript = planet.GetComponent<SatelightScript>();
        satelightScript.mainPlanet = Sun;
        satelightScript.speed = speed;
        satelightScript.rotateSpeed = speed * 10 * Time.deltaTime;

        Quaternion target = Quaternion.Euler(0, 0, 0);

        planet.transform.position = new Vector3(r, 0, 0);
        planet.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 0);

        return planet;
    }

    public GameObject addSatelights(GameObject planet, float r, float planetSize, GameObject[] Planets, float length, float speed, int countPlanets, int countMeteors)
    {
        planet.AddComponent<SatelightController>();

        var satelightContorller = planet.GetComponent<SatelightController>();
        satelightContorller.count = GetRandomNumber(0, 3);
        satelightContorller.mainPlanet = planet;
        satelightContorller.planets = Planets;
        satelightContorller.r = r;
        satelightContorller.size = planetSize;
        satelightContorller.more = length / (countPlanets + countMeteors) / 2f;
        satelightContorller.speed = speed * Time.deltaTime;
        satelightContorller.rotateSpeed = speed * Time.deltaTime;

        return planet;
    }

    public GameObject createMeteors(GameObject Sun, GameObject[] Planets, float r, float planetSize, int i, int count)
    {
        GameObject meteor = new GameObject();

        meteor.name = "Meteors " + count;

        meteor.transform.SetParent(Sun.transform);

        meteor.AddComponent<MeteorsScript>();

        var meteorScript = meteor.GetComponent<MeteorsScript>();
        meteorScript.Sun = Sun;
        meteorScript.Main = Planets;
        meteorScript.r = r;
        meteorScript.size = planetSize / 4;
        meteorScript.count = (int)(i * size * size * size);

        return meteor;
    }

    public Camera correctCamera(float length)
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam.transform.position = new Vector3(length * 2.5f, length / 1.5f, 0);

        cam.farClipPlane = length * 6;

        return cam;
    }

    Vector2 GetXY(float r, float angle)
    {
        float x = r * Mathf.Sin(angle);
        float y = r * Mathf.Cos(angle);

        return new Vector2(x, y);
    }

    public void CreateGalaxy()
    {
        if (size < 2.3f) size = 2.3f;
        int count = 0;
        float meteorPlace = 0;

        if (countMeteors != 0)
        {
            meteorPlace = countPlanets / countMeteors;
        }

        GameObject Sun = createMain(Planets, size);

        Vector3 realSize = Vector3.Scale(Sun.transform.localScale, Sun.GetComponent<MeshRenderer>().bounds.size);

        float length = (countPlanets + countMeteors) * realSize.x * Time.deltaTime * 3;
        float r = length / (countPlanets + countMeteors) * 2;

        Camera cam = correctCamera(length);

        for (int i = 0; i < countPlanets; i++)
        {
            if (speed > i) speed -= i;
            else speed = i * 3;

            r += length / (countPlanets + countMeteors) * 2f;
            float planetSize = ((float)GetRandomNumber((int)size * 10, (int)size * 11)) / 10;

            GameObject planet = createPlanet(Planets, planetSize, r, Sun);

            GameObject[] satelightPlanets = { Planets[0], Planets[2] };

            planet = addSatelights(planet, r, planetSize, satelightPlanets, length, speed, countPlanets, countMeteors);

            if (countMeteors != 0 && (i + 1) % meteorPlace == 0 && i != 0)
            {
                count++;
                r += length / (countPlanets + countMeteors) * 1.5f;
                GameObject meteor = createMeteors(Sun, Planets, r, planetSize, i, count);
            }
        }
    }

    private void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.label);
        style.fontSize = 10;

        GUI.Label(new Rect(Screen.width - 250, 10, 100, 20), "SPEED", style);
        InputSpeed = GUI.TextField(new Rect(Screen.width - 150, 10, 140, 20), InputSpeed, 25);

        GUI.Label(new Rect(Screen.width - 250, 35, 100, 20), "COUNT PLANETS", style);
        InputCountPlanets = GUI.TextField(new Rect(Screen.width - 150, 35, 140, 20), InputCountPlanets, 25);

        GUI.Label(new Rect(Screen.width - 250, 60, 100, 20), "COUNT METEORS", style);
        InputCountMeteors = GUI.TextField(new Rect(Screen.width - 150, 60, 140, 20), InputCountMeteors, 25);

        GUI.Label(new Rect(Screen.width - 250, 85, 100, 20), "ROATATION", style);
        InputRotation = GUI.TextField(new Rect(Screen.width - 150, 85, 140, 20), InputRotation, 25);

        if (GUI.Button(new Rect(Screen.width - 175, 110, 100, 20), "CREATE"))
        {
            try
            {
                speed = float.Parse(InputSpeed);
                countPlanets = Int32.Parse(InputCountPlanets);
                countMeteors = Int32.Parse(InputCountMeteors);
                rotation = float.Parse(InputRotation);
                if (speed <= 0 || countPlanets <= 0 || countMeteors <= 0 || rotation <= 0) throw new Exception();
                Destroy(GameObject.Find("Main"));
                CreateGalaxy();
            }
            catch(Exception e)
            {
            }
        }
    }

    void Start()
    {
        CreateGalaxy();
    }

    void Update()
    {
        
    }
}
