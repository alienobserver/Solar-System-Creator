using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOperations : MonoBehaviour
{
    public int age = 16;
    public string name = "Alex";
    public char sex = 'M';
    public bool isDead = false;
    public float counter = 100.0f;
    private int privateInfo = 15;
    protected string protectedInfo = "Secret Info";
    public float remNum = 1.1f;
    public Vector3 positions;

    private static readonly Random getrandom = new Random();

    //param || return
    public static int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    //param || not return
    public void func1(int x, int y)
    {
        Debug.Log(x + y);
    }

    //not param || return
    public int func2()
    {
        return 1 + 2;
    }

    //not param || not return
    public void func3()
    {
        Debug.Log("Hello World!");
    }

    void Start()
    {
        positions = new Vector3(0.0f, 0.0f, 0.0f);
        Debug.Log("Game is starting...");
        Debug.Log(counter);
    }

    void Update()
    {
        if(counter - remNum >= 0)
        {
            counter -= remNum;
            Debug.Log(counter);
        }
    }
}
