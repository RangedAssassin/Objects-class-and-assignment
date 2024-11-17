

using UnityEngine;

public class ProjectileWeapon : Weapon
{
    private GameObject projectilePrefab;

    public ProjectileWeapon(Transform tip, GameObject bulletReferance) : base(tip)
    {
        projectilePrefab = bulletReferance;
    }

    public override void Shoot()
    {
        GameObject.Instantiate(projectilePrefab, weaponTip.position, weaponTip.rotation);
    }

    public override void Reload()
    {

    }
}
