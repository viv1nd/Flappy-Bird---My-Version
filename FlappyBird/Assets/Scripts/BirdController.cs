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
    public int healthGain = 10;
    public int damage = 2;

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
            TakeDamage(damage);
        }

        if (col.tag == "Cloud")
        {
            col.gameObject.SetActive(false);
            TakeDamage(damage);
        }

        if (col.tag == "Food")
        {
            if (currentHealth <= maxHealth - healthGain)
            {
                col.gameObject.SetActive(false);
                healthBar.SetHealth(currentHealth + healthGain);
                
            }
            else if ( currentHealth > maxHealth - healthGain)
            {
                col.gameObject.SetActive(false);
                healthBar.SetHealth(maxHealth);
                
            }
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
        
        if(transform.position.y < _yaxisLimit)
        {
            _rb2d.velocity = Vector2.zero;
            _rb2d.AddForce(Vector2.up * _force);

        }
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