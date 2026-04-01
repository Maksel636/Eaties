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

    public int EnemiesAlive { get; set; } = 0;

    [SerializeField] private float _cooldownBetweenWaves;
    private float _currentCooldown = 0f;


    // Update is called once per frame
    void Update()
    {
        if (EnemiesAlive > 0)
            return;

        if (_waveIndex >= _waves.Count)
        {
            Debug.Log("End of waves");
            gameObject.SetActive(false);
            return;
        }

        if (_currentCooldown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _currentCooldown = _cooldownBetweenWaves;
            return;
        }

        _currentCooldown -= Time.deltaTime;
    }

    private IEnumerator SpawnWave()
    {
        Debug.Log("Starting Wave " + _waveIndex);

        Wave wave = _waves[_waveIndex];
        EnemiesAlive = wave.Count;

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
        EnemiesAlive--;
        _enemies.Remove(enemy);
    }

}
