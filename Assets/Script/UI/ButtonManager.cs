using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private ButtonController _resetBtn;
    [SerializeField] private ButtonController _autoKickBtn;
    [SerializeField] private ButtonController _kickBtn;
    private BallManager _ballManager;

    private void Awake()
    {
        _ballManager = FindObjectOfType<BallManager>();
        _resetBtn.Init(OnResetClicked);
        _autoKickBtn.Init(OnAutoKickClicked);
        _kickBtn.Init(OnKickClicked);
    }

    private void Start()
    {
        Debug.Log("Button Manager Awake: Subscribing to events");
        GameEvents.OnNearestTargetChanged += InvisibleKickBtn;
        _kickBtn.gameObject.SetActive(false);
    }

    private void InvisibleKickBtn(Transform nearestTarget)
    {
        if (nearestTarget == null)
        {
            _kickBtn.gameObject.SetActive(false);
        }
        else
        {
            _kickBtn.gameObject.SetActive(true);
        }
    }

    private void OnResetClicked()
    {
        ResetObjectManager.Instance.ResetAllObjectState();
    }

    //autoclick for farthest ball
    private void OnAutoKickClicked()
    {
        GameEvents.OnAutoClick?.Invoke();
    }

    //kick for nearest ball
    private void OnKickClicked()
    {
        GameEvents.OnKick?.Invoke(_ballManager.CurrentNearestBall);
    }
}
