using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private PseudorandomPoints targets;
    [SerializeField] private float imprecision = 0.2f;
    private Transform _currentTarget;
    void Start()
    {
        InitializeNavMeshAgent();
        _currentTarget = SwitchTarget();
    }
    void Update()
    {
        if (_currentTarget.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        Vector3 playerPosition = transform.position;
        Vector3 targetPosition = _currentTarget.position;
        bool isTargetApproximatelyEqualsPlayer = playerPosition.x < targetPosition.x + imprecision && playerPosition.x > targetPosition.x - imprecision && playerPosition.y < targetPosition.y + imprecision && playerPosition.y > targetPosition.y - imprecision;
        if (isTargetApproximatelyEqualsPlayer)
        {
            _currentTarget = SwitchTarget();
        }
        navMeshAgent.SetDestination(_currentTarget.position);
    }
    public Transform SwitchTarget()
    {
        Transform newTarget = targets.GetPseudorandomPoint();
        if (newTarget != _currentTarget)
        {
            return newTarget;
        }
        else
        {
            return SwitchTarget();
        }
    }
    private void InitializeNavMeshAgent()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
}
