using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    private TowerBase _activeTower = null;
    [SerializeField] private GameObject _rangerPrefab;
    [SerializeField] private GameObject _piercingPrefab;
    [SerializeField] private GameObject _octopusPrefab;
    [SerializeField] private GameObject _pigirdPrefab;
    [SerializeField] private GameObject _basicBoss;

    public bool HasActiveTower => _activeTower != null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PlaceTower(Enemy enemy)
    {
        if (_activeTower) return false;

        GameObject towerToSpawn = null;

        switch (enemy.type)
        {
            case EnemyType.Basic: towerToSpawn = _rangerPrefab; break;
            case EnemyType.Piercing: towerToSpawn = _piercingPrefab; break;
            case EnemyType.Octopus: towerToSpawn= _octopusPrefab; break;
            case EnemyType.Pigird: towerToSpawn = _pigirdPrefab; break;
            case EnemyType.BasicBoss: towerToSpawn = _basicBoss; break;
        }

        var towerObj = Instantiate(towerToSpawn, transform.position, transform.rotation);
        _activeTower = towerObj.GetComponent<TowerBase>();
        return _activeTower != null;
    }
}
