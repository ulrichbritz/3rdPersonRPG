using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
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

    public void SetMaxMana(float maxMana)
    {
        slider.maxValue = maxMana;
        slider.value = maxMana;
    }
    public void SetCurrentMana(float currentStamina)
    {
        slider.value = currentStamina;
    }
}
