using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] protected AudioClip explosionClip;
    [SerializeField] private GameObject dieEffect;
    [SerializeField] private float characterHealth;

    public Health healthValue;
    public Weapon currentWeapon;

    protected virtual void Awake()
    {
        healthValue = new Health(characterHealth);
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
        Instantiate(dieEffect, transform.position,transform.rotation);
        SoundManager.instance.PlaySound(explosionClip);
        Destroy(gameObject);
    }

    public void Interact()
    {
        //Debug.Log(GameManager.timer);
    }
    public virtual void StartAttack()
    {
        //as i press the mouse button
    }

    public virtual void Attack()
    {
        /*Debug.Log("Punching")*/;
        
    }


    public virtual void StopAttack()
    { 
    //as i release the mouse button
    }

}
