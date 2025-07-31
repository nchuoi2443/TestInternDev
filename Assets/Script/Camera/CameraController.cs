using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam1;
    [SerializeField] private CinemachineVirtualCamera _cam2;
    [SerializeField] private CinemachineVirtualCamera _cam3;
    [SerializeField] private float _timeToWaits;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _maxTrackingTime = 6f;
    [SerializeField] private float _currentTrackingTime = 0f;
    private bool _isTracking = false;

    private CinemachineBrain _brain;

    private void Awake()
    {
        _brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void Start()
    {
        GameEvents.OnKick += OnKickAction;
        GameEvents.OnBallHitGoal += OnGoldAction;
        GameEvents.AfterAutoClick += OnKickAction;
    }

    private void OnDestroy()
    {
        GameEvents.OnKick -= OnKickAction;
        GameEvents.OnBallHitGoal -= OnGoldAction;
        GameEvents.AfterAutoClick -= OnKickAction;
    }

    private void Update()
    {
        if (!_isTracking) return;
        _currentTrackingTime += Time.deltaTime;
        if (_currentTrackingTime >= _maxTrackingTime)
        {
            _isTracking = false;
            _cam1.gameObject.SetActive(true);
            _cam2.gameObject.SetActive(true);
            _currentTrackingTime = 0f;
        }
    }

    public void OnKickAction(Transform target)
    {
        _cam1.gameObject.SetActive(false);
        _cam3.Follow = target;
        _isTracking = true;
        StartCoroutine(WaitForBlendThen(() =>
        {
            _cam2.gameObject.SetActive(false);
        }));
    }

    public void OnGoldAction()
    {
        _isTracking = false;
        _currentTrackingTime = 0f;
        StartCoroutine(WaitSomeSecAfterBack(_timeToWaits));
    }

    private IEnumerator WaitSomeSecAfterBack(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        _cam1.gameObject.SetActive(true);
        _cam2.gameObject.SetActive(true);
    }

    private IEnumerator WaitForBlendThen(System.Action callback)
    {
        while (_brain.IsBlending)
            yield return null;
        callback?.Invoke();
    }


}
