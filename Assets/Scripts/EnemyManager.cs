using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemyManager : MonobehaviourSingleton<EnemyManager>
{
    [SerializeField] private GameObject _enemyPrefab;

    private List<Enemy> _enemies = new();
    public List<Enemy> Enemies => _enemies;

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
            var obj = Instantiate(_enemyPrefab);
            obj.transform.position = Path.Instance.Waypoints[0].transform.position;
            _enemies.Add(obj.GetComponent<Enemy>());
            yield return new WaitForSeconds(wave.SpawnDelay);
        }

        ++_waveIndex;
    }

    public void RegisterEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void UnRegisterEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}
