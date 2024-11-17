using UnityEngine;
using UnityEngine.Events;
public class Health
{
    public UnityEvent OnDied;
    private float healthValue;
    private Character myCharacter;
    
    public void DecreasedHealth(float damageParameter)
    {
        healthValue -= damageParameter;
        
        Debug.Log("health decreasing to: " + healthValue);
        //update the ui
        //check if is dead
        if (IsDead())
        {
            OnDied.Invoke();
            //spawn explosion or just Destroy/hide character, multiply by 2
        }
    }

    public void IncreaseHealth(float increaseParameter)
    {
        healthValue += increaseParameter;
    }

    
    public bool IsDead()
    {   //return "returns" to where its called
        return healthValue <= 0;
    }

    public float GetHealthValue()
    {
        return healthValue;
    }

    public Health()
    {
        healthValue = 100;
        OnDied = new UnityEvent();
    }

    public Health(float initialHealth)
    {
        healthValue = initialHealth;
        OnDied = new UnityEvent();
    
    }




}
