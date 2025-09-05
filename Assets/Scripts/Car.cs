using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public string brand;
    public string model;
    public int year;

    void Start()
    {
        Debug.Log("car age: " + CalculateAge(2025));
    }


    public int CalculateAge(int currentYear)
    {
        int age = currentYear - year;
        return age;
    }
}
