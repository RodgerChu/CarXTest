using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Configs.Monsters
{
    [CreateAssetMenu(fileName = "MonsterStats", menuName = "Configs/MonsterStats", order = 4)]
    public class MonsterStats : ScriptableObject
    {
        [SerializeField] private float _maxHp;
        [SerializeField] private float _speed;
        [SerializeField] private float _damageToPlayer;

        public float MaxHP => _maxHp;
        public float Speed => _speed;
        public float Damage => _damageToPlayer;
    }
}
