using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private ParticleSystem _particleSystem;
    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball == null) return;
        _particleSystem.transform.position = transform.position;
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _particleSystem.Play();
        StartCoroutine(WaitToDeactiveBall(ball, _waitTime));
        GameEvents.OnBallHitGoal?.Invoke();
    }

    private IEnumerator WaitToDeactiveBall(Ball ball, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ball.gameObject.SetActive(false);
    }
}
