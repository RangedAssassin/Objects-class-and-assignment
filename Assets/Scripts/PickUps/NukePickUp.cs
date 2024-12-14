using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickUp : Pickup
{
    [SerializeField] private GameManager gameManagerScript;
    [SerializeField] private AudioClip noNukes;

    private void Awake()
    {
        GameObject managerObject = GameObject.Find("GameManager");
        gameManagerScript = managerObject.GetComponent<GameManager>();

    }

    protected override void PickMeUp(Player playerInTrigger)
    {
        if (playerInTrigger == null)
        {
            Debug.LogError("Player reference is null!");
        }
        else if (gameManagerScript.NukeCount >= 3)
        {
            SoundManager.instance.PlaySound(noNukes);
            Destroy(gameObject);
        }
        else
        {
            SoundManager.instance.PlaySound(pickupSound);
            gameManagerScript.IncreaseNukeCount();
            Destroy(gameObject);
        }
    }
}
