using UnityEngine;
public class Player : Character
{
    [SerializeField] private Transform playerWeaponTip;


    public override void StartAttack()
    {
       base.StartAttack();
        currentWeapon.StartShooting(playerWeaponTip);
    }
    public override void Attack()
    {
        currentWeapon.Shoot(playerWeaponTip);

    }

    public override void StopAttack()
    {
        base.StopAttack();
        currentWeapon.StopShooting();
    }

}
