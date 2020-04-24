using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public static Healthbar instance { get; private set; }
    public Slider Slider;
    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        
    }

    public void SetSliderMaxValue(float value)
    {
        Slider.maxValue = value;
    }

    public void SetValue(float value)
    {
        Slider.value = value;
    }
}
