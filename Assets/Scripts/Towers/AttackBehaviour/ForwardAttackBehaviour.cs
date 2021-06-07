using System.Collections;
using System.Collections.Generic;
using TD.Factories;
using TD.Monsters;
using TD.Towers.Projectiles;
using UnityEngine;

namespace TD.Towers.AttackBehaviour
{
    public class ForwardAttackBehaviour : SimpleAttackBehaviour
    {
        public override void Attack(BaseMonster target)
        {
            var projectile = _projectilesFactory.Create();
            projectile.gameObject.transform.position = _turretBarrel.gameObject.transform.position;
            projectile.SetDirection(_turretBarrel.gameObject.transform.position + _turretBarrel.gameObject.transform.forward);

            _projectilesManager.AddUpdatable(projectile);

            _cooldownCounter = _attackCooldown;
            StartCoroutine(ReloadCoroutine());
        }
    }
}
