using System;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class HungerUI : MonoBehaviour
{
    int _hunger;
    [SerializeField] private TextMeshProUGUI _hungerText;
    [SerializeField] private TowerBase _myTower;

    private void Awake()
    {
        _myTower._onHungerChanged += UpdateUI;        
    }

    private void UpdateUI(object sender, HungerArgs hunger)
    {
        _hungerText.text = "Hunger: " + hunger._hunger;
        Debug.Log("update");
    }
}
