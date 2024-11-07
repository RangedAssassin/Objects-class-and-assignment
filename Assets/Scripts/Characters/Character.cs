using UnityEngine;
public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float movementSpeed = 10f;

    public Health healthValue;
    public Weapon currentWeapon;

    public virtual void Move(Vector2 direction)
    {
        myRigidbody.AddForce(direction * Time.deltaTime * movementSpeed,ForceMode2D.Impulse);
    }

    public void Interact()
    {

    }

    public virtual void Attack()
    {
        Debug.Log("Punching");
    }



}
