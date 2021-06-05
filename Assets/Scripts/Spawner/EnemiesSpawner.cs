using System.Collections;
using System.Collections.Generic;
using TD.Configs.Waves;
using UnityEngine;
using System;
using TD.Monsters;

namespace TD.EnemiesSpawn
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private EnemiesWaveData[] _enemiesWaves;
        [SerializeField] private MonstersManager _monstersManager;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _destination;

        private int _completedSpawnCoroutines = 0;

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            foreach(var wave in _enemiesWaves)
            {
                _completedSpawnCoroutines = 0;
                var spawnCoroutines = new List<Coroutine>();

                foreach(var enemiesWave in wave.EnemiesWaves)
                {
                    spawnCoroutines.Add(StartCoroutine(EnemySpawnCoroutine(enemiesWave)));
                }

                yield return new WaitUntil(() => _completedSpawnCoroutines == spawnCoroutines.Count);
                yield return new WaitForSeconds(wave.TimeUntilNextWave);
            }
        }

        private IEnumerator EnemySpawnCoroutine(SingleEnemyWaveData data)
        {
            var i = 0;

            if (data.SpawnOneImmideatly)
            {
                SpawnEnemy(data.MonsterPrefab);
                i++;                
            }

            for (i = 0; i < data.NumberOfUnitsToSpawn; i++)
            {
                yield return new WaitForSeconds(data.TimeSpawnInterval);
                SpawnEnemy(data.MonsterPrefab);
            }

            _completedSpawnCoroutines++;
        }

        private void SpawnEnemy(BaseMonster prefab)
        {
            var monster = Instantiate(prefab);
            monster.transform.position = _spawnPosition.position;
            monster.SetMoveDestination(_destination);

            _monstersManager.AddMonster(monster);
        }
    }
}
