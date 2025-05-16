using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NormalMonster : Monster
{
    private bool _IsActivateControl = true;
    private bool _canTracking = true;

    [SerializeField] private int MaxHp;
    private ObservableProperty<int> CurrnetHp;
    private ObservableProperty<bool> IsMoving = new();
    private ObservableProperty<bool> IsAttacking = new();

    
    private NavMeshAgent _navMeshAgent;

    [SerializeField] private Transform _targetTransform;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        HandleControl();
        //_navMeshAgent.isStopped = true;
    }

    private void Init()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void HandleControl()
    {
        if (!_IsActivateControl) return;

        HandleMove();
    }
    private void HandleMove()
    {
        if (_targetTransform == null) return;

        

        if(_canTracking)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_targetTransform.position);
            IsMoving.Value = true;
        }
        else
        {
            _navMeshAgent.isStopped = true;
            IsMoving.Value = false;
        }

    }
}
