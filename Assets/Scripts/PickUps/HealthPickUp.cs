using UnityEngine;

public class HealthPickUp : Pickup
{
    [SerializeField] private int healthPointsToAdd;


    protected override void PickMeUp(Player playerInTrigger)
    {
        playerInTrigger.healthValue.IncreaseHealth(healthPointsToAdd);
        SoundManager.instance.PlaySound(pickupSound);
        Destroy(gameObject);
    }
}

