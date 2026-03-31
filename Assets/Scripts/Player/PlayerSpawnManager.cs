using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerSpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerInputManager _playerInputManager;
    void Start()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
    }
}

