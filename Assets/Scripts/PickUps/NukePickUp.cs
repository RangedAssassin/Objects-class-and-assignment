using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickUp : Pickup
{
    [SerializeField] private GameManager gameManagerScript;

    private void Awake()
    {
        GameObject managerObject = GameObject.Find("GameManager");
        gameManagerScript = managerObject.GetComponent<GameManager>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            //Debug.Log("Collision with: " + collision.name);
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
            Debug.LogError("Player reference is null!");
        }
        else 
        {
            //Debug.Log("Player reference: " + playerInTrigger);
            //Debug.Log("nuke picked up");
            gameManagerScript.IncreaseNukeCount();
            Destroy(gameObject);
        }
    }
}
