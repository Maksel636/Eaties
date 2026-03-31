using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //EnemyManager.Instance.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EnemyManager.Instance.UnRegisterEnemy(this);
    }
}
