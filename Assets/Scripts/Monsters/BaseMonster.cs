using UnityEngine;
using TD.Hitables;
using TD.Towers.Projectiles;
using System;
using TD.Configs.Monsters;
using TD.Monsters.Move;
using TD.Monsters.Health;

namespace TD.Monsters
{
    public class BaseMonster : MonoBehaviour, IHitable
    {
        const float _destinationReachedTreshhold = 0.3f;

        [SerializeField] private MonsterStats _stats;
        [SerializeField] private BaseMonsterMoveBehaviour _moveBehaviour;
        [SerializeField] private BaseMonsterHealthBehaviour _healthBehaviour;

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
            _healthBehaviour.Initialize(_stats);
            _moveBehaviour.Initialize(_stats.Speed);
        }

        private void Start()
        {
            _moveBehaviour.AddOnDestinationReachedCallback(OnDestinationReached);
            _healthBehaviour.AddOnKilledCallback(OnKilled);
        }

        void IHitable.OnHit(BaseProjectile hitProjectile)
        {
            _currentHp -= hitProjectile.ProjectileDamage;

            if (_currentHp <= 0)
                _onKilled?.Invoke(this);
        }

        public void UpdateInternal()
        {
            _moveBehaviour.UpdateInternal();
        }

        private void OnDestinationReached()
        {
            _onDestinationReached?.Invoke(this);
            _moveBehaviour.RemoveOnDestinationReachedCallback(OnDestinationReached);
        }

        private void OnKilled()
        {
            _onKilled?.Invoke(this);
            _healthBehaviour.RemoveOnKilledCallback(OnKilled);
        }

        public void SetMoveDestination(Transform destination)
        {
            _moveBehaviour.MoveTo(destination);
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
