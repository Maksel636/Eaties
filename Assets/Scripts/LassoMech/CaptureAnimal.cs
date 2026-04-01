using System.Collections;
using UnityEngine;

public class CaptureAnimal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject _lasso;
    [SerializeField] private EscapeAnimal _escapeAnimal;
    [SerializeField] private LassoMech _lassoMech;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            StartCoroutine(StartCoroutine(other.transform));

            //_isCaptured = true;
            // other.gameObject.SetActive(false);
        }
    }
    IEnumerator StartCoroutine(Transform other)
    {
        yield return new WaitForSeconds(0.5f);
        _lasso.SetActive(false);
        _lassoMech.IsLassoing = false;
        _escapeAnimal = other.transform.GetChild(0).gameObject.GetComponent<EscapeAnimal>();
        _escapeAnimal.JoinPlayer(_lasso.transform.parent);
    }
    private void Update()
    {
        //if (_isCaptured)
        //{
        //    _lasso.SetActive(false);
        //}

    }
}
