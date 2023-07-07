using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoonController : MonoBehaviour
{
    [SerializeField] private GameObject _Sun;
    [SerializeField] private GameObject _Moon;
    [SerializeField, Range(5f, 30f)] private float _duration;
    [SerializeField] private Camera _Maincamera;
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;
    private string dayColorHex = "#006DB0";
    private string nightColorHex = "#000000";

    public bool isDay = true;
    private float timer = 0f;


    private void Start()
    {
        _Maincamera = Camera.main;
        dayColor = ParseColor(dayColorHex);
        nightColor = ParseColor(nightColorHex);
        UpdateBackgroundColor();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > _duration)
        {
            _Sun.SetActive(!isDay);
            _Moon.SetActive(isDay);

            timer = 0f;
            isDay = !isDay;
            UpdateBackgroundColor();
        }
    }

    private void UpdateBackgroundColor()
    {
        if (isDay)
        {
            _Maincamera.backgroundColor = dayColor;
        }
        else
        {
            _Maincamera.backgroundColor = nightColor;
        }
    }

    private Color ParseColor(string colorHex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(colorHex, out color);
        return color;
    }
}
