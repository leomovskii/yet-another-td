using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Targeting {

    public static bool TryPickCreepInRange(Vector3 towerPosition, float range, TowerTargetingMode targetingMode, out Creep result, List<Creep> ignoreList = null) {
        var creeps = PickAllCreepsInRange(towerPosition, range, ignoreList);
        if (creeps.Count == 0) {
            result = null;
            return false;
        }

        if (creeps.Count == 1)
            result = creeps[0];

        else if (targetingMode == TowerTargetingMode.FirstInRange)
            result = creeps.OrderBy(creep => creep.leadValue).First();

        else if (targetingMode == TowerTargetingMode.LastInRange)
            result = creeps.OrderBy(creep => creep.leadValue).Last();

        else if (targetingMode == TowerTargetingMode.Weakest)
            result = creeps.OrderBy(creep => creep.health.value).First();

        else if (targetingMode == TowerTargetingMode.Strongest)
            result = creeps.OrderBy(creep => creep.health.value).Last();

        else if (targetingMode == TowerTargetingMode.Nearest)
            result = creeps.OrderBy(creep => Vector3.Distance(towerPosition, creep.transform.position)).First();

        else if (targetingMode == TowerTargetingMode.Farthest)
            result = creeps.OrderBy(creep => Vector3.Distance(towerPosition, creep.transform.position)).Last();

        else
            result = creeps[Random.Range(0, creeps.Count)];

        return true;
    }

    public static List<Creep> PickAllCreepsInRange(Vector3 towerPosition, float range, List<Creep> ignoreList = null) {
        var result = new List<Creep>();

        var allCreepsOnMap = Object.FindObjectsOfType<Creep>();
        if (allCreepsOnMap.Length == 0)
            return result;

        bool checkIgnoreList = ignoreList != null && ignoreList.Count > 0;

        foreach (var creep in allCreepsOnMap) {
            if (checkIgnoreList && ignoreList.Contains(creep))
                continue;
            if (Vector3.Distance(towerPosition, creep.transform.position) <= range)
                result.Add(creep);
        }

        return result;
    }
}