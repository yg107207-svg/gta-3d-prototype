using UnityEngine;

public class Sniper : WeaponBase
{
    [SerializeField] private float scopeZoom = 10f;
    private bool isScoped = false;

    protected override void Start()
    {
        base.Start();
        stats.weaponName = "Sniper Rifle";
        stats.damage = 80f;
        stats.fireRate = 1.5f;
        stats.accuracy = 0.98f;
        stats.magCapacity = 5;
        stats.bulletSpeed = 80f;
        stats.range = 200f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ToggleScope();
        }
    }

    private void ToggleScope()
    {
        isScoped = !isScoped;
        // Aqui você pode adicionar lógica de câmera de mira
    }

    public override void Fire()
    {
        if (isScoped)
        {
            stats.accuracy = 0.99f; // Maior acurácia com mira
        }
        base.Fire();
    }
}