using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IResetableObject
{
    //Reset object variable
    private Vector3 _initPosition;
    private Quaternion _initRotation;

    private Vector2 _moveInput;
    private Rigidbody _rb;
    private Animator _animator;
    private NearestTargetFinder _nearestTargetFinder;
    [SerializeField] private float _findTargetRadius = 10f;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private StateMachine _stateMachine;
    public Transform NearestTarget { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _nearestTargetFinder = new NearestTargetFinder(transform, _findTargetRadius, _targetLayerMask);
    }

    private void Start()
    {
        _animator.SetBool(EnumPlayerAnimation.normal.ToString(), true);
        _animator.SetFloat(EnumPlayerAnimation.Blend.ToString(), 0f);
        _stateMachine.ChangeState(new StateIdle(), transform, _animator);
        StartCoroutine(CheckNearTargetRoutine());
        ResetObjectManager.Instance.RegisterResetObject(this);
    }

    IEnumerator CheckNearTargetRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);
            Transform nearTaret = _nearestTargetFinder.FindNearestTarget();
            if (nearTaret != NearestTarget)
            {
                NearestTarget = nearTaret;
                GameEvents.OnNearestTargetChanged?.Invoke(NearestTarget);
            }
        }
    }

    private void FixedUpdate()
    {

        _stateMachine.UpdateState(_moveInput, Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void SaveInitState()
    {
        _initPosition = transform.position;
        _initRotation = transform.rotation;
    }

    public void ResetToInitState()
    {
        transform.position = _initPosition;
        transform.rotation = _initRotation;
    }
}


public enum EnumPlayerAnimation
{
    normal,
    happy,
    angry,
    dead,
    Blend,
}