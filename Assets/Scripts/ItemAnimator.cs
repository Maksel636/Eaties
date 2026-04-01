using System;
using System.Collections;
using UnityEngine;

public class ItemAnimator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float _rotationSpeed = 30f;
    [SerializeField] private float _timer;
    [SerializeField] private float _despawnTime = 10f;  
    bool _isFading = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        _timer += Time.deltaTime;
        if (_timer > _despawnTime)
        {
            if (_isFading == false)
                FadeItem();
        }

        transform.rotation = Quaternion.Euler(0, Time.time * _rotationSpeed, 0);
    }

    private void FadeItem()
    {
        _isFading = true;
        StartCoroutine(FadeOutAndDestroy());
    }
    IEnumerator FadeOutAndDestroy()
    {
        float flikkerDuration = 0.4f; // Duration of the flickering effect

        GameObject objec = transform.GetChild(0).gameObject;
        float fadeDuration = 9; // Duration of the fade-out effect
        float elapsedTime = 0f;
        bool isActive = true; // Track the active state of the object

        while (elapsedTime < fadeDuration)
        {
            elapsedTime++;

            isActive = !isActive; // Toggle the active state

            objec.SetActive(isActive);

            yield return new WaitForSeconds(flikkerDuration);

            flikkerDuration *= 0.9f; // Reduce the flickering duration by half each time
        }
        Destroy(gameObject); // Destroy the item after fading out
    }
}
