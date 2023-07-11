using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _tappingButton;
    [SerializeField] private GameObject Game;
    [SerializeField] private Button _playButton;
    [SerializeField] private TMP_Text highScore;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text gameTimerText;
    public float currentTime;
    private bool timerStart = false;
    public BirdController birdController;
    public SunMoonController SunMoonController;

    [SerializeField] private GameObject floatingText;

    public static bool IsPlaying = false;

    private void Start()
    {
        Game.SetActive(false);
        _playButton.onClick.AddListener(OnPlay);
        UpdateHighScore();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            OnTap();
            timerStart = true;

        }
        if (timerStart == true)
        {
            currentTime += Time.deltaTime;
            //gameTimerText.text = "Total TIme : " + currentTime.ToString("0.0");
        }
    }

    private void Awake()
    {
        BirdController.onDeath += OnGameOver;
        ObstacleMover.onScore += OnScoreText;
    }

    private void OnDestroy()
    {
        BirdController.onDeath -= OnGameOver;
        ObstacleMover.onScore -= OnScoreText;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnGameOver()
    {
        IsPlaying = false;
        _restartButton.SetActive(true);
    }

    private void OnScoreText()
    {
        if (SunMoonController.isDay)
        {
            _score.text = (int.Parse(_score.text) + 1).ToString();
        }
        else
        {
            _score.text = (int.Parse(_score.text) + 2).ToString();
        }

        ChechHighScore();
    }

    private void ChechHighScore()
    {
        if (int.Parse(_score.text) > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", int.Parse(_score.text));
        }
    }

    private void UpdateHighScore()
    {
        highScore.text = $"High Score : {PlayerPrefs.GetInt("HighScore", 0)}";
    }
    public void OnTap()
    {

        if (IsPlaying == false)
        {
            OnPlay();
        }
        birdController.Flap();
    }

    public void OnPlay()
    {

        IsPlaying = true;
        Game.SetActive(true);
        _playButton.gameObject.SetActive(false);
    }

    public void ShowHealthGain(string healthGainText)
    {
        if (floatingText)
        {
            GameObject prefab = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
            prefab.GetComponentInChildren<TextMeshPro>().text = healthGainText;
        }
    }
}
