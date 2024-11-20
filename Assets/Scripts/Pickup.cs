using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Weapon pickup;
    //[SerializeField] private float increaseHealthBy;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            collision.attachedRigidbody.GetComponent<Player>().currentWeapon = pickup; 
        }
    }
}
