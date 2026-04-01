using UnityEngine;

public class FollowEndLasso : MonoBehaviour
{
    [SerializeField] private Transform _endLasso;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_endLasso != null)
        {
            transform.position = _endLasso.position;
        }
    }
}
