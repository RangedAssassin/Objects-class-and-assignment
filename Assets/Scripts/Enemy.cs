using UnityEngine;
public class Enemy : Character
{
    public override void Attack()
    {
        //base.Attack(); --Calling "Attack()" method from base class
        Debug.Log("The Enemy Bites the Player!!");
    }

    public Enemy()
    {
        speed = 10;
        healthValue = new Health(50);
        currentWeapon = new Weapon();
    }

    public Enemy(float speed, int health, int difficulty) : base(speed, health)
    {

    }
}
