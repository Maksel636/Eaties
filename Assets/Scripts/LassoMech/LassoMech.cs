using UnityEngine;
using UnityEngine.InputSystem;

public class LassoMech : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public bool IsLassoing = false;
    [SerializeField] private GameObject _lasso;
    public bool IsAnimalEscaping = false;
    private void Start()
    {
        _animator.speed = 0.5f;
    }
    public void OnLasso(InputValue value)
    {
        if(IsAnimalEscaping) return;
        _lasso.SetActive(true);

        if (value.isPressed)
        {


            if (IsLassoing)
            {
                PlayerMovement pm = gameObject.GetComponent<PlayerMovement>(); // maybe set speed to 0 or something


                _animator.SetTrigger("Trow");
            }
            

            IsLassoing = true;
            
        }
        
    }
    public void OnStopLasso(InputValue value)
    {
        IsLassoing = false;
        _lasso.SetActive(false);
    }
}