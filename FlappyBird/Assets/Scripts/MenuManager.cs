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
    //[SerializeField] private GameObject _prefab;
    [SerializeField] private TMP_Text _score;
    public BirdController birdController;
    public SunMoonController SunMoonController;

    public static bool IsPlaying = false;

    private void Start()
    {
        Game.SetActive(false);
        _playButton.onClick.AddListener(OnPlay);
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
    }
    public void OnTap()
    {
        birdController.Flap();
    }

    public void OnPlay()
    {
        IsPlaying = true;
        Game.SetActive(true);
        _playButton.gameObject.SetActive(false);
    }
        
}
