using UnityEngine;

public class BoostAnimator : MonoBehaviour
{
    [SerializeField] private BoostItem _boostItem;
    [SerializeField] private BoostView _boostView;

    [SerializeField] private RuntimeAnimatorController _bombController;
    [SerializeField] private RuntimeAnimatorController _verticalController;
    [SerializeField] private RuntimeAnimatorController _horizontalController;
    [SerializeField] private RuntimeAnimatorController _rainbowController;
    [SerializeField] private RuntimeAnimatorController _rocketController;

    [SerializeField] private Animator _animator;    

    private const string ActionKey = "action";    
    private const string ShowKey = "show";    

    private void OnEnable()
    {
        _boostItem.SetedType += OnSetedType;        

        _boostView.Hided += OnHided;
        _boostView.Showed += OnShowed;
    }


    private void OnDisable()
    {
        _boostItem.SetedType -= OnSetedType;       

        _boostView.Hided -= OnHided;
        _boostView.Showed -= OnShowed;
    }

    private void OnHided()
    {
        SetTrigger(ActionKey);
    }

    private void OnShowed()
    {
        SetTrigger(ShowKey);
    }

    private void OnSetedType(BoostTypes type)
    {
        switch (type)
        {
            case BoostTypes.None:
                break;
            case BoostTypes.Bomb:
                SetController(_bombController);
                break;
            case BoostTypes.Horizontal:
                SetController(_horizontalController);
                break;
            case BoostTypes.Vertical:
                SetController(_verticalController);
                break;
            case BoostTypes.Rainbow:
                SetController(_rainbowController);
                break;
            case BoostTypes.Rocket:
                SetController(_rocketController);
                break;
            default:
                break;
        }
    }

    private void SetController(RuntimeAnimatorController controller)
    {
        _animator.runtimeAnimatorController = controller;
    }

    private void SetTrigger(string key)
    {
        _animator.SetTrigger(key);
    }
}
