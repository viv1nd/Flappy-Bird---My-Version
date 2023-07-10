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
    [SerializeField] private GameObject floatingText;


    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private float _force;
    [SerializeField] private float _yaxisLimit;

    private void Start()
    {
        Time.timeScale = 1f;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
            //col.gameObject.SetActive(false);
            col.transform.GetChild(0).gameObject.SetActive(true);
            col.GetComponent<SpriteRenderer>().enabled = false;
            col.GetComponent<Rigidbody2D>().AddForce(_rb2d.velocity * 10);
            col.GetComponent<Rigidbody2D>().AddForce(transform.right * ObstacleSpawner.gameSpeed * 100);
            col.GetComponent<Collider2D>().enabled = false;
            TakeDamage(damage);
        }

        if (col.tag == "Food")
        {
            if (currentHealth <= maxHealth - healthGain)
            {
                col.gameObject.SetActive(false);
                healthBar.SetHealth(currentHealth + healthGain);
                ShowHealthGain(healthGain.ToString());

            }
            else if (currentHealth > maxHealth - healthGain)
            {
                col.gameObject.SetActive(false);
                healthBar.SetHealth(maxHealth);

            }
        }
    }




    public void Flap()
    {

        if (transform.position.y < _yaxisLimit)
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



    private void ShowHealthGain(string healthGainText)
    {
        if (floatingText)
        {
            GameObject prefab = Instantiate(floatingText, transform.position, Quaternion.identity,transform);
            prefab.GetComponentInChildren<TextMeshPro>().text = healthGainText;
        }
    }
}