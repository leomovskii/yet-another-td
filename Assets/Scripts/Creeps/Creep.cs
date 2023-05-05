using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreepMovement))]
[RequireComponent(typeof(CreepHealth))]
[DisallowMultipleComponent]
public class Creep : MonoBehaviour, ICreepMovement, ICreepHealth {

    private CreepMovement _CreepMovement;
    private CreepHealth _CreepHealth;

    [SerializeField] CreepType m_CreepType;

    public CreepMovement movement => _CreepMovement;
    public CreepHealth health => _CreepHealth;

    public CreepType type => m_CreepType;

    public float leadValue => _CreepMovement.GetLeadValue();

    public void Initialize(CreepMovementData movementData, Waypoint spawn, CreepHealthData healthData) {
        _CreepMovement = GetComponent<CreepMovement>();
        _CreepHealth = GetComponent<CreepHealth>();

        _CreepMovement.Initialize(movementData, spawn, this);
        _CreepHealth.Initialize(healthData, this);
    }

    public void OnCreepReachedFinish() {
        Destroy(gameObject);
    }

    public void OnCreepDied() {
        Destroy(gameObject);
    }
}