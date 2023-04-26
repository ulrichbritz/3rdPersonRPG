using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    Transform player;

    public Slider slider;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStats>().gameObject.transform;
    }

    private void Update()
    {
        
    }

    public void SetMaxStamina(float maxStamina)
    {
        slider.maxValue = maxStamina;
        slider.value = maxStamina;
    }
    public void SetCurrentStamina(float currentStamina)
    {
        slider.value = currentStamina;
    }
}
