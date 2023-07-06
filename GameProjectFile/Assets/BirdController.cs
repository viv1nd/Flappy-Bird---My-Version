using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BirdController : MonoBehaviour
{
    public static event Action onDeath;
    public static event Action onScore;
    public static event Action onTap;


    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private float _force;
    [SerializeField] private float _yaxisLimit;


    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _rb2d.position.y < _yaxisLimit)
        {
            Flap();
        }
    }

    private void OnCollisionEnter2D()
    {
        onDeath?.Invoke();

        Time.timeScale = 0f;
    }
    private void OnTriggerEnter2D()
    {
        onScore?.Invoke();
    }

    public void Flap()
    {
        _rb2d.velocity = Vector2.zero;
        _rb2d.AddForce(Vector2.up * _force);
    }
    
}
