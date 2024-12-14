using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interpolation : MonoBehaviour //use for percentages between 2 numbers
{
    public float number1;
    public float number2;

    public float interpolationValue;
    
    public float handle;

    public Vector2 position1;
    public Vector2 position2;

    public Slider slider;
    public Image healthbarimage;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MoveCloser", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //interpolation of colors for health bar
        healthbarimage.color = Color.Lerp(Color.red,Color.green, slider.value / slider.maxValue);
    }

    void MoveCloser()
    {
        interpolationValue = Mathf.Lerp(number1, number2, handle);
        //transform.position = Vector2.Lerp(position1, position2, handle);
        transform.position = Vector2.Lerp(transform.position, position2, Time.deltaTime);
    }
}
