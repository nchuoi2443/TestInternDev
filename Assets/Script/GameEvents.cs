using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action<Transform> OnKick;
    public static Action OnAutoClick;
    public static Action<Transform> AfterAutoClick;
    public static Action OnBallHitGoal;
    public static Action<Transform> OnNearestTargetChanged;

    public static void Kick(Transform target) => OnKick?.Invoke(target);
    public static void AutoClick() => OnAutoClick?.Invoke();
    public static void AfterAnAutoClick(Transform target) => AfterAutoClick?.Invoke(target);
    public static void BallHitGoal() => OnBallHitGoal?.Invoke();
    public static void NearestTargetChanged(Transform target) => OnNearestTargetChanged?.Invoke(target);
}
