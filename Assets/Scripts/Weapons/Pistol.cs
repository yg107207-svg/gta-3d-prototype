using UnityEngine;

public class Pistol : WeaponBase
{
    protected override void Start()
    {
        base.Start();
        stats.weaponName = "Pistol";
        stats.damage = 20f;
        stats.fireRate = 0.15f;
        stats.accuracy = 0.95f;
        stats.magCapacity = 15;
        stats.bulletSpeed = 50f;
    }

    public override void Fire()
    {
        base.Fire();
    }
}