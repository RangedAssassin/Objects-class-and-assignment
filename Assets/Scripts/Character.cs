using UnityEngine;
public class Character : MonoBehaviour
{
    protected float speed;
    public Health healthValue;
    public Weapon currentWeapon;

    public virtual void Move()
    {
        Debug.Log(GameManager.GlobalManager.timer);
        Debug.Log("Walking");
    }

    public void Interact()
    {

    }

    public virtual void Attack()
    {
        Debug.Log("Punching");
    }

    public Character()
    {
        speed = 5;
        healthValue = new Health();
        currentWeapon = new Weapon();
    }

    public Character(float speedParameter, int healthvalueParameter)
    {
        speed = speedParameter;
        healthValue = new Health(healthvalueParameter);
        currentWeapon = new Weapon();
    }

}
