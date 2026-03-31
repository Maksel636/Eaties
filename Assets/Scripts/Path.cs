using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Path : MonobehaviourSingleton<Path>
{
    private Transform[] _waypoints;
    public Transform[] Waypoints => _waypoints;

    protected override void Awake()
    {
        base.Awake();

        _waypoints = new Transform[transform.childCount];
        for (int idx = 0; idx < transform.childCount; ++idx)
        {
            _waypoints[idx] = transform.GetChild(idx);
        }
    }
}