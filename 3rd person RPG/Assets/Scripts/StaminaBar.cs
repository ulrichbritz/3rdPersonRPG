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

    public void SetMaxStamina(int maxStamina)
    {
        slider.maxValue = maxStamina;
        slider.value = maxStamina;
    }
    public void SetCurrentStamina(int currentStamina)
    {
        slider.value = currentStamina;
    }
}
