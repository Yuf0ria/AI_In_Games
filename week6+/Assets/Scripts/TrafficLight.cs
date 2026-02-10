using System;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum LightColor
    {
        red,
        orange,
        green
    }
    
    public LightColor current = LightColor.red;
    public TrafficLight light;

    public bool isRed => current == LightColor.red;
    public bool isOrange => current == LightColor.orange;
    public bool isGreen => current == LightColor.green;

    private void OnTriggerEnter(Collider other)
    {
        CarAI car = other.GetComponent<CarAI>();
        if (car != null)
        {
            car.SetActiveTrafficLight(light);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CarAI car = other.GetComponent<CarAI>();
        if (car != null)
        {
            car.ClearActiveTrafficLight(light);
        }
    }
}
