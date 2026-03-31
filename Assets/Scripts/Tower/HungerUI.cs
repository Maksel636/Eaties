using TMPro;
using UnityEngine;

public class HungerUI : MonoBehaviour
{
    int _hunger;
    [SerializeField] TextMeshProUGUI _hungerText;
    

    private void UpdateUI(int hunger)
    {
        _hungerText.text = "Hunger: " + hunger;
    }
}
