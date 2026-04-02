using System.Collections;
using UnityEngine;

public class CaptureAnimal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject _lasso;
    [SerializeField] private EscapeAnimal _escapeAnimal;
    [SerializeField] private LassoMech _lassoMech;
    [SerializeField] private Color _playerColor;
    private void OnTriggerEnter(Collider other)
    {
        if(_escapeAnimal != null)
            if (_escapeAnimal.IsEscaping) return;

        if (other.CompareTag("Animal") && _lassoMech.Istrowing)
        {
            _escapeAnimal = other.transform.GetChild(0).gameObject.GetComponent<EscapeAnimal>();

            if(_escapeAnimal.IsCaptured == true) return;

            StartCoroutine(StartCoroutine(other.transform));

            //_isCaptured = true;
            // other.gameObject.SetActive(false);
        }
    }
    IEnumerator StartCoroutine(Transform other)
    {
        yield return new WaitForSeconds(0.5f);
        if (_escapeAnimal != null)
            if (!_lassoMech.IsAnimalEscaping) // if not escaping escape NOW
            {
                _lasso.SetActive(false);
                _lassoMech.IsLassoing = false;
                _escapeAnimal = other.transform.GetChild(0).gameObject.GetComponent<EscapeAnimal>();
                _escapeAnimal.JoinPlayer(_lasso.transform.parent, _playerColor);
            }


    }
    private void Update()
    {
        if (_lassoMech.IsAnimalEscaping && _escapeAnimal == null)
        {
            
            _lassoMech.ResetLasso();
        }

    }
}
