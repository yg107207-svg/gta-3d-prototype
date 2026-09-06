using UnityEngine;

public class Motorcycle : VehicleBase
{
    protected override void Start()
    {
        base.Start();
        stats.vehicleName = "Motorcycle";
        stats.maxSpeed = 80f;  // Mais rápido
        stats.acceleration = 10f; // Mais ágil
        stats.turnSpeed = 6f;
        stats.maxHealth = 100f; // Menos durabilidade
    }
}