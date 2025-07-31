using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IState
{
    float _currentBlendValue;
    Animator _animator;
    private StateMachine _stateMachine;
    private Transform _playerTransform;
    public void EnterState(Transform playerTrans, Animator animator)
    {
        _playerTransform = playerTrans;
        _stateMachine = playerTrans.GetComponent<StateMachine>();
        _animator = animator;
        _currentBlendValue = _animator.GetFloat(EnumPlayerAnimation.Blend.ToString());
        Rigidbody _rb = playerTrans.GetComponent<Rigidbody>();
    }

    public void ExitState()
    {
        
    }

    public void UpdateState(Vector2 currentInput, float deltaTime)
    {

        if (currentInput != Vector2.zero)
        {
            _stateMachine.ChangeState(new StateRun(), _playerTransform, _animator);
            return;
        }
        
    }
}
