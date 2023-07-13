using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject Game;
    [SerializeField] private Button _TapButton;
    [SerializeField] private TMP_Text highScore;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _score_GameEnd;
    //[SerializeField] private TMP_Text gameTimerText;
    public float currentTime;
    private bool timerStart = false;
    public BirdController birdController;
    public SunMoonController SunMoonController;

    //[SerializeField] private GameObject floatingText;

    [SerializeField] private GameObject gameOverScreen;

    public static bool IsPlaying = false;

    private void Start()
    {
        Game.SetActive(false);
        _TapButton.onClick.AddListener(OnPlay);
        UpdateHighScore();
    }

    private void Update()
    {
        //timerStart = true;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnTap();
        }
        /*if (timerStart == true && IsPlaying == true)
        {
            currentTime += Time.deltaTime;
            gameTimerText.text = "Total Game Time : " + currentTime.ToString("0.0");
        }*/
    }

    private void OnEnable()
    {
        BirdController.onDeath += OnGameOver;
        ObstacleMover.onScore += OnScoreText;
    }

    private void OnDisable()
    {
        BirdController.onDeath -= OnGameOver;
        ObstacleMover.onScore -= OnScoreText;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        gameOverScreen.SetActive(false);
    }
    private void OnGameOver()
    {
        IsPlaying = false;
        timerStart = false;
        UpdateHighScore();
        _restartButton.SetActive(true);
        gameOverScreen.SetActive(true);
        
    }

    private void OnScoreText()
    {
        if (SunMoonController.isDay)
        {
            _score.text = (int.Parse(_score.text) + 1).ToString();
            _score_GameEnd.text = (int.Parse(_score.text) + 1).ToString();
        }
        else
        {
            _score.text =  (int.Parse(_score.text) + 2).ToString();
            _score_GameEnd.text =  (int.Parse(_score.text) + 2).ToString();
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
        highScore.text = $"{PlayerPrefs.GetInt("HighScore", 0)}";
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
        _TapButton.onClick.RemoveAllListeners();
        _TapButton.onClick.AddListener(OnTap);
        IsPlaying = true;
        Game.SetActive(true);
        startText.gameObject.SetActive(false);
        //_TapButton.gameObject.SetActive(false);
    }

    public void ShowHealthGain(string healthGainText)
    {
        //if (floatingText)
       // {
        //    GameObject prefab = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        //    prefab.GetComponentInChildren<TextMeshPro>().text = healthGainText;
        //}
    }
}
