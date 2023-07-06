using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _tappingButton;
    [SerializeField] private TMP_Text _score;
    public BirdController birdController;

    private void Awake()
    {
        BirdController.onDeath += OnGameOver;
        BirdController.onScore += OnScoreText;
    }

    private void OnDestroy()
    {
        BirdController.onDeath -= OnGameOver;
        BirdController.onScore -= OnScoreText;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnGameOver()
    {
        _restartButton.SetActive(true);
    }

    private void OnScoreText()
    {
        _score.text = (int.Parse(_score.text) + 1).ToString();
    }
     public void OnTap()
    {
        //_tappingButton.SetActive(true);
        birdController.Flap();
    }

}
