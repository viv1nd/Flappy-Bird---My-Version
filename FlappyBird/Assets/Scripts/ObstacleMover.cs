using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleMover : MonoBehaviour
{
    public static event Action onScore;

    [SerializeField] private float _speed;
    [SerializeField] private float _xaxisLimit;

    private void Update()
    {
        this.transform.position -= Vector3.right * _speed * Time.deltaTime;

        if (this.transform.position.x < _xaxisLimit)
        {
            onScore?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
