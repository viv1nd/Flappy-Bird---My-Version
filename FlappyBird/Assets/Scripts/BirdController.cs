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
    [SerializeField] private GameObject foodPick;

    //[SerializeField] private SunMoonController dayNightController;
    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private Transform bird;
    [SerializeField] private Animator birdAnimator;
    [SerializeField] private float _force;
    [SerializeField] private float _yaxisLimit;
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;
    [SerializeField] private Transform HealthReduceOverlay_UI;
    [SerializeField] private float playAreMaxHeight = 5.7f;

    private void Start()
    {
        Time.timeScale = 1f;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnEnable()
    {
        HealthManager.onHealthZero += DisableMe;
    }

    private void DisableMe()
    {
        HealthReduceOverlay_UI.gameObject.SetActive(false);
        onDeath?.Invoke();
    }

    private void Update()
    {
        if(transform.position.y > playAreMaxHeight)
        {
            SecondsTracker();
        }
        else if(HealthReduceOverlay_UI.gameObject.activeInHierarchy)
        {
            BackInPlayArea();
        }
    }

    bool ReducingHealth = false;
    private void SecondsTracker()
    {
        if(ReducingHealth == false)
        {
            StartCoroutine(OutOfPlayArea());
        }
    }

    IEnumerator OutOfPlayArea()
    {
        ReducingHealth = true;
        yield return new WaitForSeconds(1f);
        TakeDamage(5);
        HealthReduceOverlay_UI.gameObject.SetActive(true);
        SoundManager.Instance.Play(SoundManager.Sounds.CloudBurst);
        ReducingHealth = false;
    }

    private void BackInPlayArea()
    {
        StopCoroutine(OutOfPlayArea());
        HealthReduceOverlay_UI.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        HealthManager.onHealthZero -= DisableMe;
    }

    private void OnCollisionEnter2D()
    {
        onDeath?.Invoke();
        birdAnimator.Play("Bird_Dead");
        SoundManager.Instance.Play(SoundManager.Sounds.PlayerDeath);
        //Time.timeScale = 0f;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Tree"))
        {
            TakeDamage(damage);
            SoundManager.Instance.Play(SoundManager.Sounds.BranchSound);
        }

        if (col.CompareTag("Cloud"))
        {
            //col.gameObject.SetActive(false);
            col.transform.GetChild(0).gameObject.SetActive(true);
            col.GetComponent<SpriteRenderer>().enabled = false;
            //col.GetComponent<Rigidbody2D>().AddForce(_rb2d.velocity * 10);
            //col.GetComponent<Rigidbody2D>().AddForce(transform.right * ObstacleSpawner.gameSpeed * 100);
            col.GetComponent<Collider2D>().enabled = false;
            TakeDamage(damage);
            SoundManager.Instance.Play(SoundManager.Sounds.CloudBurst) ;
        }

        if (col.CompareTag("Food"))
        {
            GameObject effect = Instantiate(foodPick, this.transform);
            if (currentHealth <= maxHealth - healthGain)
            {
                col.gameObject.SetActive(false);
                healthBar.SetHealth(currentHealth + healthGain);
                ShowHealthGain(healthGain.ToString());
                SoundManager.Instance.Play(SoundManager.Sounds.FoodtakeSound);

            }
            else if (currentHealth > maxHealth - healthGain)
            {
                col.gameObject.SetActive(false);
                healthBar.SetHealth(maxHealth);
                SoundManager.Instance.Play(SoundManager.Sounds.FoodtakeSound);

            }
        }
    }


    

    public void Flap()
    {

        if (bird.transform.position.y < _yaxisLimit)
        {
            _rb2d.velocity = Vector2.zero;
            _rb2d.AddForce(Vector2.up * _force);
            birdAnimator.Play("Bird_Swing");
            SoundManager.Instance.Play(SoundManager.Sounds.FlapSound);
        }
    }

    private void TakeDamage(int damage)
    {
        if(currentHealth - damage > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            currentHealth = 0;
        }
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