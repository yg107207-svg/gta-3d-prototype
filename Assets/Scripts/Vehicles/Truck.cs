using UnityEngine;

public class Truck : VehicleBase
{
    protected override void Start()
    {
        base.Start();
        stats.vehicleName = "Truck";
        stats.maxSpeed = 40f;  // Mais lento
        stats.acceleration = 4f;
        stats.turnSpeed = 2f;
        stats.maxHealth = 300f; // Muito resisténte
    }
}