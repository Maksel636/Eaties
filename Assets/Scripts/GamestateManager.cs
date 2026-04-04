using System;
using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateManager : MonoBehaviour
{
    private float _waitForReturnTime = 1.5f;
    [SerializeField] GameObject _winText;
    [SerializeField] GameObject _loseText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameHealth.Instance.CurrentHealth = GameHealth.Instance.StartingHealth;
        EnemyManager.Instance.ResetGame();
        GameHealth.Instance.AddObserver(CheckGameOver);
        _loseText.SetActive(false);
        _winText.SetActive(false);
    }

    private void CheckGameOver(int health)
    {
        if(health <= 0)
        {
            _loseText.SetActive(true);
            Audio.Instance.InitiateSound(Audio.SoundType.LostGame);
            StartCoroutine(ReturnMainMenu());
            EnemyManager.Instance.CanSpawn = false;
        }
    }

    private void Update()
    {
      //CheckGameWon();
    }

    private void CheckGameWon()
    {
        if(EnemyManager.Instance.EnemiesAlive <= 0 && EnemyManager.Instance.WaveIndex >= EnemyManager.Instance.NrWaves)
        {
            _winText.SetActive(true);
            StartCoroutine(ReturnMainMenu());
            EnemyManager.Instance.CanSpawn = false;
        }
    }

    private IEnumerator ReturnMainMenu()
    {
        yield return new WaitForSeconds(_waitForReturnTime);
        SceneManager.LoadScene("StartScene");
    }
}
