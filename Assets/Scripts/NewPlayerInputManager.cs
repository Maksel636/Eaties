using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

public class SimplePlayerJoinManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;

    private int playerCount = 0;
    private HashSet<InputDevice> usedDevices = new HashSet<InputDevice>();

    private IDisposable buttonPressListener; // IMPORTANT

    private void OnEnable()
    {
        buttonPressListener = InputSystem.onAnyButtonPress.Call(OnAnyButtonPress);
    }

    private void OnDisable()
    {
        buttonPressListener?.Dispose(); // Correct way to remove
    }

    private void OnAnyButtonPress(InputControl control)
    {
        InputDevice device = control.device;

        if (usedDevices.Contains(device))
            return;

        if (playerCount >= playerPrefabs.Length)
            return;

        if (!(device is Gamepad) && !(device is Keyboard))
            return;

        JoinPlayer(device);
    }

    private void JoinPlayer(InputDevice device)
    {
        usedDevices.Add(device);

        GameObject prefab = playerPrefabs[playerCount];

        Transform spawn = spawnPoints.Length > playerCount
            ? spawnPoints[playerCount]
            : transform;

        GameObject player = Instantiate(prefab, spawn.position, spawn.rotation);

        var input = player.GetComponent<PlayerInput>();
        if (input != null)
        {
            input.SwitchCurrentControlScheme(device);
        }

        Debug.Log($"Player {playerCount + 1} joined with {device.displayName}");

        playerCount++;
    }
}