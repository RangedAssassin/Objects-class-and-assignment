using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            PickMeUp(collision.attachedRigidbody.GetComponent<Player>());
        }
    }

    protected abstract void PickMeUp(Player playerInTrigger);
}
