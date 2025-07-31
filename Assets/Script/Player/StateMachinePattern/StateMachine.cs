using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState _currentState;

    public void ChangeState(IState newState, Transform playerTrans, Animator animator)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState(playerTrans, animator);
    }

    public void UpdateState(Vector2 moveInput, float deltaTime)
    {
        _currentState?.UpdateState(moveInput, deltaTime);
    }

}
