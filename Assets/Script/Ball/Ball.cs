using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IResetableObject
{
    private Vector3 _initPosition;
    private Quaternion _initRotation;
    private Vector3 _initVelocity;

    private Rigidbody _rigidbody;
    [SerializeField] private float _kickSpeed = 10f;
    private BallManager _ballManager;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _ballManager = FindObjectOfType<BallManager>();
    }

    private void Start()
    {
        ResetObjectManager.Instance.RegisterResetObject(this);
        _ballManager.AddBall(this);
    }


    public void GotKicked(Transform targetGold)
    {
        Vector3 direction = (targetGold.position - transform.position).normalized;
        _rigidbody.velocity = direction * _kickSpeed;
    }

    public void ResetToInitState()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = _initPosition;
        transform.rotation = _initRotation;
        _rigidbody.angularVelocity = Vector3.zero;
        gameObject.SetActive(true);
    }

    public void SaveInitState()
    {
        _initVelocity = _rigidbody.velocity;
        _initPosition = transform.position;
        _initRotation = transform.rotation;
    }
}
