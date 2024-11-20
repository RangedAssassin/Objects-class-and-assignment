using UnityEngine;
public class Player : Character
{
    [SerializeField] private Transform playerWeaponTip;

    public override void Attack()
    {
        currentWeapon.Shoot(playerWeaponTip);
    }
}
