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
        [SerializeField] protected float _attackCooldown = 0.5f;
        [SerializeField] protected ProjectilesManager _projectilesManager;
        [SerializeField] protected Transform _turretBarrel;

        protected abstract IFactory<BaseProjectile> _projectilesFactory { get; }

        protected float _cooldownCounter = 0f;

        public bool CanAttack => _cooldownCounter <= 0;

        public virtual void Attack(BaseMonster target)
        {
            var projectile = _projectilesFactory.Create();
            projectile.transform.position = _turretBarrel.transform.position;
            projectile.SetDirection(target.transform.position);

            _projectilesManager.AddUpdatable(projectile);

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
