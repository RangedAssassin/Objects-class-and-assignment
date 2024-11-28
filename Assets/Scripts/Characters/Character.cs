using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float movementSpeed = 10f;

    public Health healthValue;
    public Weapon currentWeapon;

    protected virtual void Start()
    {
        healthValue = new Health(10);
        healthValue.OnDied.AddListener(PlayDeadEffect);
        
        
    }

    public virtual void Move(Vector2 direction)
    {
        myRigidbody.AddForce(direction * Time.deltaTime * movementSpeed, ForceMode2D.Impulse);
    }

    public virtual void Look(Vector2 direction)
    {
        float angle; // = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Vector2.SignedAngle(Vector2.up, direction);
        myRigidbody.SetRotation(angle);
    }

    public virtual void PlayDeadEffect()
    {
        Destroy(gameObject);
    }

    public void Interact()
    {
        //Debug.Log(GameManager.timer);
    }

    public virtual void Attack()
    {
        Debug.Log("Punching");
    }

}
