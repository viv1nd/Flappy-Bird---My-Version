using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class BirdController : MonoBehaviour
{
    public static event Action onDeath;
    
    public static event Action onTap;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthManager healthBar;

    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private float _force;
    [SerializeField] private float _yaxisLimit;

    private void Start()
    {
        Time.timeScale = 1f;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Tree")
        {
             TakeDamage(2);
        }

        if (col.tag == "Cloud")
        {
            col.gameObject.SetActive(false);
            TakeDamage(2);
        }
    }

    //private void OnTriggerExit2D(Collider2D col)
    //{
        //if (col.tag == "Obstacle")
        //{
            //onScore?.Invoke();
        //}
    //}


    public void Flap()
    {
        _rb2d.velocity = Vector2.zero;
        _rb2d.AddForce(Vector2.up * _force);
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void Death()
    {
        if(currentHealth == 0)
        {
            onDeath?.Invoke();

            Time.timeScale = 0f;
        }
    }
}