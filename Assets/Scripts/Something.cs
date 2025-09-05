using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something : MonoBehaviour
{
    private int num1 = 1;
    private int num2 = 2;

    void Start()
    {
        Debug.Log(Compare());
    }


    public string Compare()
    {
        return (num1 > num2) ? "something" : "something else";
    }
}
