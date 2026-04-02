using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Renderer _healthRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameHealth.Instance.AddObserver(UpdateHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealth(int newHealth)
    {
        _healthText.text = "Health: " + newHealth;

        _healthRenderer.material.SetFloat("_FillAmount", (float)newHealth/10);
    }
}
