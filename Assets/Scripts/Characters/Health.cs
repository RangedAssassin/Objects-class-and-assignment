using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Health
{
    [SerializeField] private Slider healthSlider;
    public UnityEvent<float> OnHealthChanged;
    public UnityEvent OnDied;
    private float healthValue;
    private float maxHealth = 100f;
    private Character myCharacter;
    
    void Start()
    {
        healthSlider.GetComponent<Slider>().value = healthValue;    
    }
    public void DecreasedHealth(float damageParameter)
    {
        healthValue -= damageParameter;       
        OnHealthChanged.Invoke(healthValue);
        
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
        healthValue = Mathf.Clamp(healthValue,0f,maxHealth);
        OnHealthChanged.Invoke(healthValue);
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
        OnDied = new UnityEvent();
        OnHealthChanged = new UnityEvent<float>();
    }

    public Health(float initialHealth)
    {
        healthValue = initialHealth;
        OnDied = new UnityEvent();
        OnHealthChanged = new UnityEvent<float>();
    }




}
