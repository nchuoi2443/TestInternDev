using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestTargetFinder
{
    private Transform _playerTransform;
    private float _searchRadius;
    private LayerMask _targetLayerMask;

    public NearestTargetFinder(Transform playerTransform, float searchRadius, LayerMask targetLayerMask)
    {
        _playerTransform = playerTransform;
        _searchRadius = searchRadius;
        _targetLayerMask = targetLayerMask;
    }

    public Transform FindNearestTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_playerTransform.position, _searchRadius, _targetLayerMask);
        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;
        foreach (var hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(_playerTransform.position, hitCollider.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTarget = hitCollider.transform;
            }
        }
        return nearestTarget;
    }
}
