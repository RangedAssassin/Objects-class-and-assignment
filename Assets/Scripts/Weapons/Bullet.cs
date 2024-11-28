using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myrigidbody2d;
    [SerializeField] public float bulletSpeed;
    [SerializeField] private string targetTag;

    [SerializeField] private float myDamage;

    private bool shouldRotate = false;
    [SerializeField] private float rotationSpeed = 0f;
    [SerializeField] private float acceleration = 0f;

    //[SerializeField] private float clampMinSpeed = 2f;
    //[SerializeField] private float clampMaxSpeed = 10f;

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
        if (shouldRotate == true)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);


            bulletSpeed += acceleration * Time.deltaTime;
            //bulletSpeed = Mathf.Clamp(bulletSpeed, clampMinSpeed, clampMaxSpeed);

            // Continuously move outward based on the updated rotation
            Vector2 outwardDirection = transform.up;
            myrigidbody2d.velocity = outwardDirection * bulletSpeed;
        }
    }

    public void RotateBullet(bool enable)
    {
        shouldRotate = enable;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.CompareTag(targetTag))
        {
            collision.rigidbody.GetComponent<Character>().healthValue.DecreasedHealth(myDamage);
        }
        
        Destroy(gameObject);
    }
}
