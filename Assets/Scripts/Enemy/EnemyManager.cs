using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemyManager : MonobehaviourSingleton<EnemyManager>
{
    private List<Enemy> _enemies = new();
    public List<Enemy> Enemies => _enemies;

    private List<Wave> _waves;
    private int _waveIndex = 0;
    public int WaveIndex => _waveIndex;
    public int NrWaves
    {
        get { return _waves.Count; }
    }

    public int EnemiesAlive { get; set; } = 0;

    private float _cooldownBetweenWaves = 1f;
    private float _currentCooldown = 0f;

    private void Start()
    {
        var waveData = FindFirstObjectByType<WaveData>();
        if (waveData)
        {
            _waves = waveData.Waves;
        }
    }

    public void ResetGame()
    {
        _waveIndex = 0;
        EnemiesAlive = 0;
        _currentCooldown = 0f;

        CanSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemiesAlive > 0)
            return;

        if (_waveIndex >= _waves.Count)
        {
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

    bool _canSpawn = true;
    public bool CanSpawn
    {
        get { return _canSpawn; }
        set { _canSpawn = value; }
    }
    private IEnumerator SpawnWave()
    {
        if (_canSpawn == false) yield break; // Don't spawn when not in game scene

        Wave wave = _waves[_waveIndex];
        EnemiesAlive = wave.Count;

        for (int idx = 0; idx < wave.Count; ++idx)
        {
            var obj = Instantiate(wave.EnemyPrefab);
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
