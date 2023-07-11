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
    public float visibleHealth;
    public int targetHealth;
    public GameObject _particleSystem;
    public Text sliderValue_Debug;

    public Animator HealthReduceVFX;
    private void Start()
    {

    }
    private void Update()
    {
        if((int)visibleHealth > targetHealth)
        {
            visibleHealth = visibleHealth - (10 * Time.deltaTime);
            healthSlider.value = visibleHealth;
            RectTransform baseTransform = GetComponent<RectTransform>();
            float percentWidth = visibleHealth / healthSlider.maxValue;
            float mappedValue = percentWidth * baseTransform.sizeDelta.x;
            sliderValue_Debug.text = mappedValue.ToString();
            _particleSystem.GetComponent<RectTransform>().anchoredPosition = new Vector2(mappedValue, _particleSystem.GetComponent<RectTransform>().anchoredPosition.y);
            
            HealthReduceVFX.SetBool("Reducing", true);
        }
        else if((int)visibleHealth < targetHealth)
        {
            visibleHealth = visibleHealth + (10 * Time.deltaTime);
            healthSlider.value = visibleHealth;
            RectTransform baseTransform = GetComponent<RectTransform>();
            float percentWidth = visibleHealth / healthSlider.maxValue;
            float mappedValue = percentWidth * baseTransform.sizeDelta.x;
            sliderValue_Debug.text = mappedValue.ToString();
            _particleSystem.GetComponent<RectTransform>().anchoredPosition = new Vector2(mappedValue, _particleSystem.GetComponent<RectTransform>().anchoredPosition.y);

        }
        else
        {
            HealthReduceVFX.SetBool("Reducing", false);
        }
    }
    public void SetHealth(int health)
    {
        // it is setting the value of health bar (UI) directly
        //healthSlider.value = health;
        // it is giving us a target value which should be shouwn in health bar (UI) eventually(along some time period)
        targetHealth = health;
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
        visibleHealth = health;
        targetHealth = health;
        fill.color = gradient.Evaluate(1f);
    }
}
