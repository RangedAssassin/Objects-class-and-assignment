using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinPickUp : Pickup
{
    private ScoreManager scoreManager;

    private void Awake()
    {
            scoreManager = FindObjectOfType<ScoreManager>();

    }

    protected override void PickMeUp(Player playerInTrigger)
    {
        scoreManager.IncreaseScore(ScoreType.CoinCollected);

        SoundManager.instance.PlaySound(pickupSound);
        Destroy(gameObject);
    }
}
