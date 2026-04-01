using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    private TowerBase _activeTower = null;
    [SerializeField] private GameObject _rangerPrefab;
    
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

        var towerObj = Instantiate(_rangerPrefab, transform.position, transform.rotation);
        _activeTower = towerObj.GetComponent<TowerBase>();
        return _activeTower != null;
    }
}
