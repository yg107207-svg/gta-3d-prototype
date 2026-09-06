using UnityEngine;

public class Submachine : WeaponBase
{
    protected override void Start()
    {
        base.Start();
        stats.weaponName = "Submachine Gun";
        stats.damage = 15f;
        stats.fireRate = 0.08f;
        stats.accuracy = 0.85f;
        stats.magCapacity = 30;
        stats.bulletSpeed = 45f;
    }
}
