using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class LassoMech : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public bool IsLassoing = false;
    [SerializeField] private GameObject _lasso;
    public bool IsMyAnimalEscaping = false;
    public bool Istrowing = false;
    private float _onesecTimer = 0f;
    private CaptureAnimal _captureAnimal;
    private void Start()
    {
        _captureAnimal = GetComponentInChildren<CaptureAnimal>();
        _animator.speed = 0.5f;
    }
    public void OnLasso(InputValue value)
    {
        if(IsMyAnimalEscaping) return;
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
        if (IsMyAnimalEscaping) // if the animal is escaping but not captured yet, reset everything
        {
            ResetLasso();
            Debug.Log("reset lasso");
            _captureAnimal.OnStopLasso();
        }
        IsLassoing = false;
        _lasso.SetActive(false);
    }
    public void ResetLasso()
    {
        IsMyAnimalEscaping = false;
        IsLassoing = false;
        _lasso.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);

    }

}