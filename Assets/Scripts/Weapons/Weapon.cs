using UnityEngine;

public abstract class Weapon
{

    private float damage;
    private int ammo;
    private float fireRate;

    protected Transform weaponTip;

    public abstract void Shoot();

    public abstract void Reload();

    public bool HasAmmo()
    {
        return ammo > 0;
    }

    public Weapon(Transform tip)
    {
        weaponTip = tip;
    }
}
