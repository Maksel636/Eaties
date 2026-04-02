using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class LassoMech : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public bool IsLassoing = false;
    [SerializeField] private GameObject _lasso;
    public bool IsAnimalEscaping = false;
    public bool Istrowing = false;
    private float _onesecTimer = 0f;
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
                Istrowing = true;
                PlayerMovement pm = gameObject.GetComponent<PlayerMovement>(); // maybe set speed to 0 or something
                _animator.SetTrigger("Trow");
                Audio.Instance.InitiateSound(Audio.SoundType.Lasso);
                _onesecTimer = 1f;
            }
            

            IsLassoing = true;
            
        }
        
    }
    private void Update()
    {
        if (Istrowing)
        {
            _onesecTimer -= Time.deltaTime;
            if (_onesecTimer <= 0f)
            {
                Istrowing = false;
            }
        }
    }

    public void OnStopLasso(InputValue value)
    {
        IsLassoing = false;
        _lasso.SetActive(false);
    }
    public void ResetLasso()
    {
        IsAnimalEscaping = false;
        IsLassoing = false;
        _lasso.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}