using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemyManager : MonobehaviourSingleton<EnemyManager>
{
    [SerializeField] private GameObject _enemyPrefab;

    private List<GameObject> _enemies = new();
    public List<GameObject> Enemies => _enemies;

    [SerializeField] private List<Wave> _waves;
    private int _waveIndex = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemies.Count > 0)
            return;

        if (_waveIndex >= _waves.Count)
        {
            Debug.Log("End of waves");
            gameObject.SetActive(false);
            return;
        }

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        Wave wave = _waves[_waveIndex];

        for (int idx = 0; idx < wave.Count; ++idx)
        {
            _enemies.Add(Instantiate(_enemyPrefab));
            yield return new WaitForSeconds(wave.SpawnDelay);
        }

        ++_waveIndex;
    }

    public void UnRegisterEnemy(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }
}
