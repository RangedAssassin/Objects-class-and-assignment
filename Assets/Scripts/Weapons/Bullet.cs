using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myrigidbody2d;
    [SerializeField] private float bulletSpeed;

    private float myDamage;
    
    void Start()
    {
        myrigidbody2d.velocity = transform.up * bulletSpeed;
        Destroy(gameObject, 5f);
    }

    public void InitializeBullet(float damageParam)
    {
        myDamage = damageParam;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.CompareTag("Enemy"))
        {
            collision.rigidbody.GetComponent<Character>().healthValue.DecreasedHealth(myDamage);
        }
        
        Destroy(gameObject);
    }
}
