using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRun : IState
{
    private StateMachine _stateMachine;
    private float _rotationSpeed = 20f;
    private Transform _playerTransform;
    private Animator _animator;
    private Rigidbody _rb;
    private float _elapseTime = 0;
    private float _maxSpeed = 7f;
    private float _currentSpeed;
    private float _acceleration = 3f;
    private float _blendValue;

    public void EnterState(Transform playerTrans, Animator animator)
    {
        _stateMachine = playerTrans.GetComponent<StateMachine>();
        _playerTransform = playerTrans;
        _animator = animator;
        _rb = playerTrans.GetComponent<Rigidbody>();
        _currentSpeed = 4f;
        _animator.SetFloat("Blend", _currentSpeed/_maxSpeed);
    }

    public void ExitState()
    {
        
    }

    public void UpdateState(Vector2 currentInput, float deltaTime)
    {
        if (currentInput == Vector2.zero)
        {
            _currentSpeed = 0;
            _blendValue = 0f;
        }
        else
        {
            _currentSpeed += _acceleration * deltaTime;
            _currentSpeed = Mathf.Min(_currentSpeed, _maxSpeed);

            Vector3 direction = new Vector3(currentInput.x, 0f, currentInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _playerTransform.rotation = Quaternion.RotateTowards(
                _playerTransform.rotation,
                targetRotation,
                _rotationSpeed
            );
        }

        _blendValue = Mathf.InverseLerp(0f, _maxSpeed, _currentSpeed);
        _animator.SetFloat("Blend", _blendValue);
        Vector3 velocity = new Vector3(currentInput.x, 0f, currentInput.y).normalized * _currentSpeed;
        _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);

        if (_currentSpeed <= 0.1f)
        {
            _stateMachine.ChangeState(new StateIdle(), _playerTransform, _animator);
            return;
        }
    }


}
