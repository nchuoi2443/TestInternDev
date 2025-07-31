using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void EnterState(Transform playerTrans, Animator animator);
    public void UpdateState(Vector2 currentInput, float deltaTime);
    public void ExitState();
}
