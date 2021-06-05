using UnityEngine;
using System.Collections;
using TD.Hitables;
using TD.Towers.Projectiles;
using System;
using TD.Configs.Monsters;

namespace TD.Monsters
{
    public class BaseMonster : MonoBehaviour, IHitable
    {
        const float _destinationReachedTreshhold = 0.3f;

        [SerializeField] private MonsterStats _stats;

        private Transform _destination;
        private float _currentHp;
        private Action<BaseMonster> _onDestinationReached;
        private Action<BaseMonster> _onKilled;

        public float Speed => _stats.Speed;
        public float Damage => _stats.Damage;
        public float CurrentHp => _currentHp;

        void Awake()
        {
            _currentHp = _stats.MaxHP;
        }

        void IHitable.OnHit(BaseProjectile hitProjectile)
        {
            _currentHp -= hitProjectile.ProjectileDamage;

            if (_currentHp <= 0)
                _onKilled?.Invoke(this);
        }

        public void UpdateInternal()
        {
            if (_destination == null)
                return;

            if (Vector3.Distance(transform.position, _destination.position) <= _destinationReachedTreshhold)
            {
                _onDestinationReached?.Invoke(this);
                _destination = null;
                return;
            }

            var translation = _destination.position - transform.position;
            if (translation.magnitude > _stats.Speed)
            {
                translation = translation.normalized * _stats.Speed * Time.deltaTime;
            }
            transform.Translate(translation);
        }

        public void SetMoveDestination(Transform destination)
        {
            _destination = destination;
        }

        public void AddOnKilledCallback(Action<BaseMonster> callback)
        {
            _onKilled += callback;
        }

        public void RemoveOnKilledCallback(Action<BaseMonster> callback)
        {
            _onKilled -= callback;
        }

        public void AddOnDestionationReachedCallback(Action<BaseMonster> callback)
        {
            _onDestinationReached += callback;
        }

        public void RemoveOnDestionReachedCallback(Action<BaseMonster> callback)
        {
            _onDestinationReached -= callback;
        }
    }
}
