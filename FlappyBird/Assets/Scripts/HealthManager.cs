using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthManager : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;
    public GameObject _restartButton;
    public static event Action onDeath;

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);

        if(health <= 0)
        {
            onDeath?.Invoke();

            Time.timeScale = 0f;
            _restartButton.SetActive(true);
        }
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
}
