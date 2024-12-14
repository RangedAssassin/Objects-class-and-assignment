

using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Projectile Weapon")]
public class ProjectileWeapon : Weapon
{
    [SerializeField] private Bullet projectilePrefab;
    [SerializeField] private AudioClip laserShootingSound;
    [SerializeField] protected bool isShooting;
 
    private float shootDelay;
    public override void StartShooting(Transform weaponTip)
    {
        isShooting = true;
        shootDelay += 1/fireRate;
    }
    public override void StopShooting()
    {
        isShooting = false;
    }
    public override void Shoot(Transform weaponTip)
    {
        if (isShooting)
        {
            if (shootDelay >= 1/fireRate)
            {
                Bullet bulletClone = GameObject.Instantiate(projectilePrefab, weaponTip.position, weaponTip.rotation);
                bulletClone.InitializeBullet(damage);
                SoundManager.instance.PlaySound(laserShootingSound);
                shootDelay = 0;
            }
            else
            {
                shootDelay += Time.deltaTime;
            }
        }
    }
    //    public virtual void StartAttack()
    //    {

    //    }

    //    public virtual void StopAttack()
    //    {

    //    }

    public override void Reload()
    {

    }


}
