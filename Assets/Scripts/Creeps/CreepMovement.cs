using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CreepMovement : MonoBehaviour {

    private CreepMovementData _CreepMovementData;
    private ICreepMovement _CreepFinishedEventListener;
    private Waypoint _TargetWaypoint;

    [HideInInspector] public float speedFactor = 1;

    public void Initialize(CreepMovementData movementData, Waypoint spawn, ICreepMovement listener) {
        _CreepMovementData = movementData;
        _TargetWaypoint = spawn;
        _CreepFinishedEventListener = listener;
    }

    private void FixedUpdate() {
        if (Vector3.Distance(transform.position, _TargetWaypoint.transform.position) > 0)
            transform.position = Vector3.MoveTowards(transform.position, _TargetWaypoint.transform.position, _CreepMovementData.baseSpeed * speedFactor * Time.fixedDeltaTime);
        else if (_TargetWaypoint.TryPickNextWaypoint(_CreepMovementData.waypointValue, out Waypoint next))
            _TargetWaypoint = next;
        else
            _CreepFinishedEventListener?.OnCreepReachedFinish();
    }

    public float GetLeadValue() {
        var target = _TargetWaypoint;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        while (target.TryPickNextWaypoint(_CreepMovementData.waypointValue, out Waypoint next)) {
            target = next;
            distance += Vector3.Distance(transform.position, target.transform.position);
        }
        return distance;
    }
}