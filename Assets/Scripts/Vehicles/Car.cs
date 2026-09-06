using UnityEngine;

public class Car : VehicleBase
{
    protected override void Start()
    {
        base.Start();
        stats.vehicleName = "Car";
        stats.maxSpeed = 60f;
        stats.acceleration = 8f;
        stats.turnSpeed = 4f;
        stats.maxHealth = 200f;
    }
}