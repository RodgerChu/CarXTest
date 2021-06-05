using System;
using System.Collections;
using System.Collections.Generic;
using TD.Monsters;
using UnityEngine;

namespace TD.Towers.Detectors
{
    [RequireComponent(typeof(SphereCollider))]
    public class SimpleEnemiesDetector: EnemyDetector
    {
        [SerializeField] private float _detectRadius = 15f;

        public override List<BaseMonster> Monsters => _monsters;

        private SphereCollider _triggerCollider;
        private List<BaseMonster> _monsters = new List<BaseMonster>();

        private void Awake()
        {
            _triggerCollider = GetComponent<SphereCollider>();
            _triggerCollider.radius = _detectRadius;
            _triggerCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {            
            var monster = other.gameObject.GetComponent<BaseMonster>();
            if (monster)
            {
                _monsters.Add(monster);
                monster.AddOnKilledCallback(OnMonsterDisapeared);
                monster.AddOnDestionationReachedCallback(OnMonsterDisapeared);
                _onMonsterAppearCallback?.Invoke(monster);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var monster = other.gameObject.GetComponent<BaseMonster>();
            if (monster)
            {
                OnMonsterDisapeared(monster);
            }
        }

        private void OnMonsterDisapeared(BaseMonster monster)
        {
            monster.RemoveOnDestionReachedCallback(OnMonsterDisapeared);
            monster.RemoveOnKilledCallback(OnMonsterDisapeared);

            _monsters.Remove(monster);
            _onMonsterDisappearCallback?.Invoke(monster);
        }
    }
}
