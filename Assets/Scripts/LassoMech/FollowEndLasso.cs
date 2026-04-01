using UnityEngine;

public class FollowEndLasso : MonoBehaviour
{
    [SerializeField] private Transform _endLasso;
    [SerializeField] private float _forwardMovement = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_endLasso != null)
        {
            transform.position = _endLasso.position + _endLasso.parent.transform.up * _forwardMovement;

        }
    }
}
