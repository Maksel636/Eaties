using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _howToPlayerButton;
    [SerializeField] Button _returnToMainButton;
    [SerializeField] GameObject _howToPlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _howToPlay.SetActive(false);

        _playButton.onClick.AddListener(PlayGame);
        _howToPlayerButton.onClick.AddListener(LoadHowToPlay);
        _returnToMainButton.onClick.AddListener(LoadMain);
    }

    private void LoadMain()
    {
        _howToPlay.SetActive(false);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("Level");
        GameHealth.Instance.CurrentHealth = GameHealth.Instance.StartingHealth;
        EnemyManager.Instance.ResetGame();
    }

    private void LoadHowToPlay()
    {
        _howToPlay.SetActive(true);
    }

}
