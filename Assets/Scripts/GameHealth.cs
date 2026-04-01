using System;
using UnityEngine;

public class GameHealth : MonobehaviourSingleton<GameHealth>
{
    private int _startingHealth = 10;

    private int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    private Action<int> _onDamage;

    protected override void Awake()
    {
        _currentHealth = _startingHealth;
    }

    public void TakeDamage()
    {
        _currentHealth -= 10;
        _onDamage?.Invoke(_currentHealth);

    }

    public void AddObserver(Action<int> observer)
    {
        _onDamage += observer;
        _onDamage?.Invoke(_currentHealth);
    }

}
