

using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Projectile Weapon")]
public class ProjectileWeapon : Weapon
{
    [SerializeField] private Bullet projectilePrefab;

    public override void Shoot(Transform weaponTip)
    {
        Bullet bulletClone = GameObject.Instantiate(projectilePrefab, weaponTip.position, weaponTip.rotation);
        bulletClone.InitializeBullet(damage);
    }

    public override void Reload()
    {

    }
}
