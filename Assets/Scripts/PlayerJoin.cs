using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoin : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerPrefabs;

    private PlayerInputManager _inputManager = null;
    private int _nextPlayerIndex = 1;

    private void Awake()
    {
        _inputManager = GetComponent<PlayerInputManager>();

        _inputManager.playerPrefab = _playerPrefabs[0];
    }

    public void OnPlayerJoin(PlayerInput input)
    {
        _inputManager.playerPrefab = _playerPrefabs[_nextPlayerIndex];
        ++_nextPlayerIndex;
    }
}
