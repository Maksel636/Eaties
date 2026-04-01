using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCountJoining : MonoBehaviour
{
    private int _playerCount = 0;
    void Start()
    {
        
    }
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.transform.GetChild(_playerCount).gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
