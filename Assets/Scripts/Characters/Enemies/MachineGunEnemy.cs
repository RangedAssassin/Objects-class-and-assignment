using System.Collections;
using UnityEngine;

public class MachineGunEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform weaponTip;
    [SerializeField] private float fireRate = 5f; // Default fire rate

    private Coroutine shootingCoroutine;

    protected override void Start()
    {
        base.Start();
        shootingCoroutine = StartCoroutine(ShootBullets()); // Start shooting
    }

    public override void Attack()
    {
        // Instantiate the bullet at the weapon tip's position and rotation
        Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
    }

    private IEnumerator ShootBullets()
    {
        float interval = 1f / fireRate; // Calculate interval between shots
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(interval);
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
