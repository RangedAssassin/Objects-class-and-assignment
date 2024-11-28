using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBulletScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletRigidbody2D;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;

    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody2D.velocity = transform.up * bulletSpeed;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health.DecreasedHealth(bulletDamage);
        }
    }
}
