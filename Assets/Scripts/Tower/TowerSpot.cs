using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    private TowerBase _activeTower = null;
    [SerializeField] private GameObject _rangerPrefab;
    [SerializeField] private GameObject _piercingPrefab;
    [SerializeField] private GameObject _octopusPrefab;
    [SerializeField] private GameObject _pigirdPrefab;
    [SerializeField] private GameObject _basicBoss;
    [SerializeField] private GameObject _jeffBoss;
    [SerializeField] private GameObject _rauBoss;
    [SerializeField] private GameObject _pigirdBoss;


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
            case EnemyType.Eve: towerToSpawn = _rangerPrefab; break;
            case EnemyType.Jeff: towerToSpawn = _piercingPrefab; break;
            case EnemyType.Rau: towerToSpawn= _octopusPrefab; break;
            case EnemyType.Pigird: towerToSpawn = _pigirdPrefab; break;
            case EnemyType.EveBoss: towerToSpawn = _basicBoss; break;
            case EnemyType.JeffBoss: towerToSpawn = _jeffBoss; break;
            case EnemyType.RauBoss: towerToSpawn = _rauBoss; break;
            case EnemyType.PigirdBoss: towerToSpawn = _pigirdBoss; break;
        }

        var towerObj = Instantiate(towerToSpawn, transform.position, transform.rotation);
        _activeTower = towerObj.GetComponent<TowerBase>();
        return _activeTower != null;
    }
}
