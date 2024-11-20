using UnityEngine;

public abstract class Weapon : ScriptableObject
{

    [SerializeField] protected float damage;
    [SerializeField] protected int ammo;
    [SerializeField] protected float fireRate;

    //public void StartShooting()
    //{
    //    bool isShooting
    //}

    //public void StopShooting()
    //{

    //}
    public abstract void Shoot(Transform weaponTip);

    public abstract void Reload();

    public bool HasAmmo()
    {
        return ammo > 0;
    }
}
