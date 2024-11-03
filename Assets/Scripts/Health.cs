public class Health
{
    private float healthValue;

    public void DecreasedHealth(float damageParameter)
    {
        healthValue -= damageParameter;
        //update the ui
        //check if is dead
        if (IsDead())
        {
            //spawn explosion
        }
    }

    public void IncreaseHealth(float increaseParameter)
    {
        healthValue += increaseParameter;
    }

    public bool IsDead()
    {
        //return "returns" to where its called
        return healthValue <= 0;
    }

    public float GetHealthValue()
    {
        return healthValue;
    }

    public Health()
    {
        healthValue = 100;
    }

    public Health(float initialHealth)
    {
        healthValue = initialHealth;
    }
}
