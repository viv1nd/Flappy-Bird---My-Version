using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoonController : MonoBehaviour
{
    [SerializeField] private Transform _Sun;
    [SerializeField] private Transform _Moon;
    [SerializeField, Range(5f, 60f)] private float _duration;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
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

    private float x = 10;
    private float y = 0;
    private float xmax = 10;
    
    //private float ymax;
    //private float ymin;

    private void Update()
    {
        if (MenuManager.IsPlaying)
        {
            timer += Time.deltaTime;

            if (timer > _duration)
            {
                isDay = !isDay;
                
                timer = 0f;
                UpdateBackgroundColor();
            }

            DoPlanetAnimation(_Sun, isDay);
            DoPlanetAnimation(_Moon, !isDay);

            //x*x + y*y = radius*radius
        }

    }

    private void DoPlanetAnimation( Transform animatedObj, bool isYPositive)
    {
        x = animatedObj.transform.position.x;
        if (isYPositive)
        {
            x = x - 2 * (xmax / (_duration * (1 / Time.deltaTime)));
            float calc = xmax * xmax - x * x;
            if (calc == 0)
                y = 0;
            else if (calc < 0)
                y = -1 * Mathf.Sqrt(calc * -1);
            else if (calc > 0)
                y = Mathf.Sqrt(calc);
            Vector3 position = new Vector3(x, y, animatedObj.position.z);
            animatedObj.localPosition = position;
        }
        else
        {
            Vector3 position = new Vector3(xmax, 0, animatedObj.position.z);
            animatedObj.localPosition = position;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0f);
    }

    /*private void Update()
    {
        timer += Time.deltaTime;

        if (timer > _duration)
        {
            isDay = !isDay;
            timer = 0f;
            UpdateBackgroundColor();

            if (!isDay)
            {
                StartCoroutine(AnimateObject(_Moon.transform, _endPosition.position, _startPosition.position, _duration));
                StartCoroutine(AnimateObject(_Sun.transform, _startPosition.position, _endPosition.position, _duration));
            }
            else
            {
                StartCoroutine(AnimateObject(_Moon.transform, _startPosition.position, _endPosition.position, _duration));
                StartCoroutine(AnimateObject(_Sun.transform, _endPosition.position, _startPosition.position, _duration));
            }
        }
    }*/

    private IEnumerator AnimateObject(Transform target, Vector3 startPosition, Vector3 endPosition, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float curveValue = _animationCurve.Evaluate(t);
            target.position = Vector3.Lerp(startPosition, endPosition, curveValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.position = endPosition;
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
