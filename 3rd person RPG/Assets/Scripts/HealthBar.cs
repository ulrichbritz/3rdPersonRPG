using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public bool isCharacterHealthBar;
    Transform player;

    public Slider slider;

    private void Start()
    {
        if (isCharacterHealthBar)
        {
            player = FindObjectOfType<PlayerStats>().gameObject.transform;
        }
    }

    private void Update()
    {
        transform.LookAt(player);
    }

    public void SetMaxHealth(int maxHP)
    {
        slider.maxValue = maxHP;
        slider.value = maxHP;
    }
    public void SetCurrentHealth(int currentHP)
    {
        slider.value = currentHP;
    }
}
