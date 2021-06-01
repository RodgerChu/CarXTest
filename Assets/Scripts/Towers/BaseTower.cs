using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TD.Towers.Detectors;
using TD.Towers.AttackBehaviour;
using TD.Monsters;

namespace TD.Towers
{
    public class BaseTower : MonoBehaviour
    {
        [SerializeField] private EnemyDetector _enemiesDetector;
        [SerializeField] private BaseAttackBehaviour _attackBehaviour;

        private Coroutine _attackCoroutine;

        protected virtual void OnEnable()
        {
            _attackCoroutine = StartCoroutine(AttackCoroutine());
        }

        protected virtual void OnDisable()
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        protected virtual IEnumerator AttackCoroutine()
        {
            while(true)
            {
                yield return new WaitUntil(() => _attackBehaviour.CanAttack);
                yield return new WaitUntil(() => _enemiesDetector.Monsters.Count != 0);

                AttackMonter(_enemiesDetector.Monsters[0]);
            }
        }

        protected virtual void AttackMonter(BaseMonster monster)
        {
            _attackBehaviour.Attack(monster);
        }
    }
}
