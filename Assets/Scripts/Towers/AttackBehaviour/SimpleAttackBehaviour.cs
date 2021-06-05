using System.Collections;
using System.Collections.Generic;
using TD.Configs.Projectiles;
using TD.Factories;
using TD.Towers.Projectiles;
using UnityEngine;

namespace TD.Towers.AttackBehaviour
{
    [RequireComponent(typeof(SimpleProjectileFactory))]
    public class SimpleAttackBehaviour : BaseAttackBehaviour
    {
        protected override IFactory<BaseProjectile> _projectilesFactory => _factory;

        private SimpleProjectileFactory _factory;

        private void Awake()
        {
            _factory = GetComponent<SimpleProjectileFactory>();
        }
    }
}
