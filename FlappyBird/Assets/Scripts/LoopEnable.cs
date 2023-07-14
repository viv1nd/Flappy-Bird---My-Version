using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopEnable : MonoBehaviour
{
    [SerializeField] private Transform enabledObject;
    [SerializeField] private float gapTime;
    [SerializeField] private float animationTime;
    bool animating = false;

    void Update()
    {
        if (!animating)
        {
            StartCoroutine(EnableObject());
        }
    }

    IEnumerator EnableObject()
    {
        animating = true;
        enabledObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(animationTime);
        enabledObject.gameObject.SetActive(false);
        yield return new WaitForSeconds(gapTime);
        animating = false;
    }


}
