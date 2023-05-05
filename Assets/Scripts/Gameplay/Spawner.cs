using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private bool _Spawning;

    [SerializeField] Waypoint m_Spawn;
    [SerializeField] Creep m_CreepPrefab;
    [SerializeField] int m_CurrentWave;
    [SerializeField] int m_MaxWaypointValue;
    [Space(10)]
    [SerializeField] SpawnData[] m_Waves;

    public void SpawnWave() {
        if (!_Spawning && m_CurrentWave < m_Waves.Length)
            StartCoroutine(SpawningRoutine());
    }

    private IEnumerator SpawningRoutine() {
        _Spawning = true;

        var levelData = m_Waves[m_CurrentWave];
        var timer = new WaitForSeconds(levelData.spawnDelay);
        int spawned = 0;
        while (spawned < levelData.countToSpawn) {
            yield return timer;
            var creep = Instantiate(levelData.prefab, m_Spawn.transform.position, Quaternion.identity);
            creep.Initialize(levelData.movementData, m_Spawn, levelData.healthData);
            spawned++;
        }
        m_CurrentWave++;

        _Spawning = false;
    }
}