using System.Collections;
using System.Collections.Generic;
using TD.Factories;
using TD.Monsters;
using TD.Towers.Projectiles;
using UnityEngine;

namespace TD.Towers.AttackBehaviour
{
    public abstract class BaseAttackBehaviour : MonoBehaviour
    {
        [SerializeField] private float _attackCooldown = 0.5f;
        [SerializeField] private BaseProjectile _projectilePrefab;
        [SerializeField] private ProjectilesManager _projectilesManager;

        protected abstract IFactory<BaseProjectile> _projectilesFactory { get; }

        private float _cooldownCounter = 0f;

        public bool CanAttack => _cooldownCounter <= 0;

        public virtual void Attack(BaseMonster target)
        {
            var projectile = _projectilesFactory.Create();
            projectile.SetDirection(target.transform);

            _projectilesManager.AddProjectile(projectile);

            _cooldownCounter = _attackCooldown;
            StartCoroutine(ReloadCoroutine());
        }

        protected virtual IEnumerator ReloadCoroutine()
        {
            yield return new WaitUntil(() => {
                _cooldownCounter -= Time.deltaTime;
                return CanAttack;
            });
        }
    }
}
