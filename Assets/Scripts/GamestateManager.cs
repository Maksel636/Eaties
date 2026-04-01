using System;
using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateManager : MonobehaviourSingleton<GamestateManager>
{
    private float _waitForReturnTime = 1.5f;
    //[SerializeField] GameObject _winText;
    //[SerializeField] GameObject _loseText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameHealth.Instance.AddObserver(CheckGameOver);
        //_loseText.SetActive(false);
        //_winText.SetActive(false);
    }

    private void CheckGameOver(int health)
    {
        if(health <= 0)
        {
            //_loseText.SetActive(true);
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
