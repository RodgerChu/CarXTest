using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Configs.Waves
{
    [CreateAssetMenu(fileName = "EnemiesWaveData", menuName = "Configs/EnemiesWaveData", order = 1)]
    public class EnemiesWaveData : ScriptableObject
    {
        [SerializeField] private SingleEnemyWaveData[] _enemiesWaves;
        [SerializeField] private float _timeUntilNextWave = 5f;

        public SingleEnemyWaveData[] EnemiesWaves => _enemiesWaves;
        public float TimeUntilNextWave => _timeUntilNextWave;
    }
}
