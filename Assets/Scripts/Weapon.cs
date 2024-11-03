

public class Weapon
{
    private float damage;
    private int ammo;
    //private int maxAmmo;
    //private int currentAmmo;
    private float fireRate;

    public void Shoot()
    {
        if (HasAmmo())
        {
            //shoot logic
            ammo--;

            //set bullet with damage

            //fireRate here to enable next shoot
        }
    }

    public void Reload()
    {

    }

    public bool HasAmmo()
    {
        return ammo > 0;
    }


}
