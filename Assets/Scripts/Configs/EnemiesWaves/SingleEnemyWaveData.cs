using System.Collections;
using System.Collections.Generic;
using TD.Monsters;
using UnityEngine;

namespace TD.Configs.Waves
{
    [CreateAssetMenu(fileName = "SingleEnemyWaveData", menuName = "Configs/SingleEnemyWaveData", order = 0)]
    public class SingleEnemyWaveData : ScriptableObject
    {
        [SerializeField] private int _numberOfUnitsToSpawn = 10;
        [SerializeField] private BaseMonster _monsterPrefab;

        [Space]
        [SerializeField] private float _timeSpawnInterval = 1f;
        [SerializeField] private float _spawnStartDelay = 0f;
        [SerializeField] private bool _spawnOneImmideatly = false;

        public int NumberOfUnitsToSpawn => _numberOfUnitsToSpawn;
        public BaseMonster MonsterPrefab => _monsterPrefab;

        public float TimeSpawnInterval => _timeSpawnInterval;
        public float SpawnStartDelay => _spawnStartDelay;
        public bool SpawnOneImmideatly => _spawnOneImmideatly;
    }
}
