using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] protected AudioClip pickupSound;

    private void Start()
    {
        Destroy(gameObject, 15f);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            PickMeUp(collision.attachedRigidbody.GetComponent<Player>());

        }
    }

    protected abstract void PickMeUp(Player playerInTrigger);
}
