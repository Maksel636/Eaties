using UnityEngine;
using UnityEngine.InputSystem;

public class LassoMech : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public bool IsLassoing = false;

    public void OnLasso(InputValue value)
    {
        if (value.isPressed)
        {
            IsLassoing = IsLassoing ? false : true;
            _animator.ResetTrigger("Rotate");
            _animator.ResetTrigger("Trow");

            if (IsLassoing)
               _animator.SetTrigger("Rotate");
            else
                _animator.SetTrigger("Trow");
            

            
        }
        
    }
}