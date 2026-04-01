using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject EnemyPrefab;
    public int Count;
    public float SpawnDelay;
}

public class WaveData : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    public List<Wave> Waves => _waves;
}
