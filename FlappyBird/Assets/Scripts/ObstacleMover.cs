using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleMover : MonoBehaviour
{
    public static event Action onScore;
    
    [SerializeField] private float _speed;
    [SerializeField] private float _xaxisScoreLimit = -9f;
    [SerializeField] private float _xaxisGameEndLimit = -15f;
    private bool _scored = false;

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

        if (this.transform.localPosition.x < _xaxisScoreLimit && !_scored)
        {
            onScore?.Invoke();
            _scored = true;

        }
        if (this.transform.localPosition.x < _xaxisGameEndLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
