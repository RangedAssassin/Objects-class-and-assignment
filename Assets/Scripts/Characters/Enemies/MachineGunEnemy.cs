//using System.Collections;
//using UnityEngine;

//public class MachineGunEnemy : Enemy
//{
//    [SerializeField] private GameObject bulletPrefab;
//    [SerializeField] private Transform weaponTip;
//    [SerializeField] private float fireRate = 5f; // Default fire rate

//    private Coroutine shootingCoroutine;

//    protected override void Start()
//    {
//        base.Start();
//        shootingCoroutine = StartCoroutine(ShootBullets()); // Start shooting
//    }

//    public override void Attack()
//    {

//        // Instantiate the bullet at the weapon tip's position and rotation
//        Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
//    }

//    private IEnumerator ShootBullets()
//    {
//        float timer = 0f;
//        float interval = 1f / fireRate; // Calculate interval between shots
//        if (timer < interval)
//        {
//            Attack();
//            yield return new WaitForSeconds(interval);
//            timer = 0f;
//        }
//        else
//        {
//            timer += Time.deltaTime;

//        }
//    }

//    private void OnDisable()
//    {
//        if (shootingCoroutine != null)
//        {
//            StopCoroutine(shootingCoroutine); // Stop shooting when the enemy is disabled
//        }
//    }
//}

using System.Collections;
using UnityEngine;

public class TimedShooterEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform weaponTip;
    [SerializeField] private float fireInterval = 5f; // Shoots every 5 seconds

    private Coroutine shootingCoroutine;

    protected override void Start()
    {
        base.Start();
        shootingCoroutine = StartCoroutine(ShootBullets()); // Start the shooting routine
    }

    public override void Attack()
    {
        // Instantiate a bullet at the weapon tip's position and rotation
        Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
    }

    private IEnumerator ShootBullets()
    {
        while (true)
        {
            Attack(); // Shoot a bullet
            yield return new WaitForSeconds(fireInterval); // Wait for the fire interval
        }
    }

    private void OnDisable()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine); // Stop shooting when the enemy is disabled
        }
    }
}

