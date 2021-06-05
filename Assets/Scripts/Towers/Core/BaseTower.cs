using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TD.Towers.Detectors;
using TD.Towers.AttackBehaviour;
using TD.Monsters;
using TD.Towers.TrackBehaviour;
using System;
using System.Linq;

namespace TD.Towers
{
    public class BaseTower : MonoBehaviour
    {
        [SerializeField] protected EnemyDetector _enemiesDetector;
        [SerializeField] protected BaseAttackBehaviour _attackBehaviour;

        protected BaseMonster _targetMonster;
        protected Coroutine _attackCoroutine;

        protected List<Func<bool>> _shootConditions = new List<Func<bool>>();

        private void Awake()
        {
            InitializeShootCoditions();
        }

        protected virtual void InitializeShootCoditions()
        {
            _shootConditions.Add(() => _attackBehaviour.CanAttack);
            _shootConditions.Add(() => _targetMonster != null);
        }

        protected virtual void OnEnable()
        {
            _attackCoroutine = StartCoroutine(AttackCoroutine());
            _enemiesDetector.AddOnMonsterAppearCallback(OnMonsterAppear);
            _enemiesDetector.AddOnMonsterDisappearCallback(OnMonsterDisappear);
        }

        protected virtual void OnDisable()
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;

            _enemiesDetector.RemoveOnMonsterAppearCallback(OnMonsterAppear);
            _enemiesDetector.RemoveOnMonsterDisappearCallback(OnMonsterDisappear);
        }

        protected virtual IEnumerator AttackCoroutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => _shootConditions.All(condition => condition()));
                AttackMonter(_targetMonster);
            }
        }

        protected virtual void AttackMonter(BaseMonster monster)
        {
            _attackBehaviour.Attack(monster);
        }

        protected virtual void OnMonsterAppear(BaseMonster monster)
        {
            if (!_targetMonster)
            {
                _targetMonster = monster;
            }
        }

        protected virtual void OnMonsterDisappear(BaseMonster monster)
        {
            if (_targetMonster == monster)
            {
                _targetMonster = _enemiesDetector.NextMonsterToAttack;
            }
        }
    }
}
