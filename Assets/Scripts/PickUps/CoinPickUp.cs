using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickUp : Pickup
{
    private ScoreManager scoreManager;

    private void Awake()
    {
            scoreManager = FindObjectOfType<ScoreManager>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            Player player = collision.attachedRigidbody.GetComponent<Player>();
            if (player != null)
            {
                PickMeUp(player);
            }
        }
    }
    protected override void PickMeUp(Player playerInTrigger)
    {
        scoreManager.IncreaseScore(ScoreType.CoinCollected);
        Destroy(gameObject);
    }
}
