using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] List<Car> cars = new List<Car>();

    private void Start()
    {
        FindOlderCar(cars);
    }

    private void FindOlderCar(List<Car> cars)
    {
        Car olderCar = cars //using linq instead of a foreach to run through the list of cars
            .OrderByDescending(car => car.CalculateAge(2025)) //sorts the list of cars from oldest to newest 
            .First(); // picks the first in the sorted collection


        //Car olderCar = car1.CalculateAge(2025) > car2.CalculateAge(2025) ? car1 : car2; //only works if i have 2 cars 
        Debug.Log($"the older car is {olderCar.brand} {olderCar.model} made in {olderCar.year} making it {olderCar.CalculateAge(2025)}");
    }
}
