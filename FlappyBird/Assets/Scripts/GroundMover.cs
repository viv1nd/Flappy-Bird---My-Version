using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundMover : MonoBehaviour
{
    
    [SerializeField] private float _speed;
    [SerializeField] private float _xaxisGameEndLimit = -15f;
    

    private void OnEnable()
    {
        BirdController.onDeath += DisableMe;
    }

    private void DisableMe()
    {
        this.enabled = false;
    }

    private void OnDisable()
    {
        BirdController.onDeath -= DisableMe;
    }

    private void Update()
    {
        this.transform.localPosition -= Vector3.right * _speed * ObstacleSpawner.gameSpeed * Time.deltaTime;

       
        if (this.transform.localPosition.x < _xaxisGameEndLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
