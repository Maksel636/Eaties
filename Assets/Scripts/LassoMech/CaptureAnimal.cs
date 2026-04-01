using UnityEngine;

public class CaptureAnimal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private LassoMech _lassoMech;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            if(_lassoMech.IsLassoing) return; // if turning lasso dont capture

            other.gameObject.SetActive(false);
        }
    }
}
