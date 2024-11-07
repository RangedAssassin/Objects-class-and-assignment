using UnityEngine;
public class Enemy : Character
{
    public override void Attack()
    {
        //base.Attack(); --Calling "Attack()" method from base class
        Debug.Log("The Enemy Bites the Player!!");
    }


}
