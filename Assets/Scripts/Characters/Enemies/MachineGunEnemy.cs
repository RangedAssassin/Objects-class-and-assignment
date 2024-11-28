using System.Collections;
using UnityEngine;

public class MachineGunEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] weaponTip;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("Attack", 1f, 1f);
    }

    protected override void Update()
    {
        if (!target) return;

        Vector2 destination = target.transform.position;
        Vector2 currentPosition = transform.position;
        Vector2 directionClass = destination - currentPosition;
        if (Vector2.Distance(destination, currentPosition) > distanceToStop)
        {
            Move(directionClass.normalized);
        }

        Look(directionClass.normalized);
    }

    public override void Attack()
    {
        foreach (Transform firepoint in weaponTip)
        {
            // Instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.RotateBullet(true);
            }
        }
    }
}

