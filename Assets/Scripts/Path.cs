using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Path Instance { get; set; }

    private Transform[] _waypoints;
    public Transform[] Waypoints => _waypoints;

    private void Awake()
    {
        if (Instance)
        {
            Debug.LogError("Trying to create second instance of Singleton class");
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this);

        _waypoints = new Transform[transform.childCount];
        for (int idx = 0; idx < transform.childCount; ++idx)
        {
            _waypoints[idx] = transform.GetChild(idx);
        }
    }
}