using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickUp : Pickup
{
    private GameManager gameManager;

    private void Awake()
    {
            gameManager = GetComponent<GameManager>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            Debug.Log("Collision with: " + collision.name);
            Player player = collision.attachedRigidbody.GetComponent<Player>();
            if (player != null)
            {
                PickMeUp(player);
            }
        }
    }
    protected override void PickMeUp(Player playerInTrigger)
    {
        if (playerInTrigger == null)
        {
            Debug.LogError("Player reference ids null!");
        }
        else 
        {
            Debug.Log("Player reference: " + playerInTrigger);
            Debug.Log("nuke picked up");
            gameManager.IncreaseNukeCount();
            Destroy(gameObject);
        }
    }
}
