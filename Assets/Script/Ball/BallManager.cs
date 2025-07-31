using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private List<Ball> _balls = new List<Ball>();
    private PlayerController _playerController;
    [SerializeField] private List<Transform> _goldTarget;
    public Transform CurrentNearestBall;
    public Transform CurrentFarthestBall;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        GameEvents.OnKick += GotKicked;
        GameEvents.OnNearestTargetChanged += GetNearestBall;
        GameEvents.OnAutoClick += GotAutoCLicked;
    }

    private void OnDestroy()
    {
        GameEvents.OnKick -= GotKicked;
        GameEvents.OnNearestTargetChanged -= GetNearestBall;
        GameEvents.OnAutoClick -= GotAutoCLicked;
    }

    private void GetNearestBall(Transform nearestTarget)
    {
        CurrentNearestBall = nearestTarget;
    }

    public void GotKicked(Transform target)
    {
        Ball ball = CurrentNearestBall.GetComponent<Ball>();
        ball?.GotKicked(GetGoldTarget(ball.transform));
    }

    public void GotAutoCLicked()
    {
        Transform ballTrans = GetFaresTarget();
        if (ballTrans == null || !ballTrans.gameObject.activeSelf) return;
        Ball ball  = ballTrans.GetComponent<Ball>();
        ball?.GotKicked(GetGoldTarget(ball.transform));

        GameEvents.AfterAutoClick(ballTrans);
    }

    Transform GetFaresTarget()
    {
        float farthestDistance = 0f;
        Transform farthestTarget = null;
        foreach ( Ball ball in _balls)
        {
            if (ball == null || ball.gameObject.activeSelf == false) continue;

            float distance = Vector3.Distance(_playerController.transform.position, ball.transform.position);
            if (distance > farthestDistance)
            {
                farthestDistance = distance;
                farthestTarget = ball.transform;
            }
        }
        CurrentFarthestBall = farthestTarget;
        return farthestTarget;
    }

    Transform GetGoldTarget(Transform currentBall)
    {
        float nearestDistance = Mathf.Infinity;
        Transform nearestTarget = null;
        
        foreach(Transform target in _goldTarget)
        {
            float distance = Vector3.Distance(currentBall.position, target.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTarget = target;
            }
        }
        return nearestTarget;
    }

    public void AddBall(Ball ball)
    {
        if (ball != null && !_balls.Contains(ball))
        {
            _balls.Add(ball);
        }
    }
}
