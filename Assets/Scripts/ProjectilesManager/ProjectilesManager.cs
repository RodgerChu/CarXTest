using System.Collections.Generic;
using TD.Managers;
using UnityEngine;

namespace TD.Towers.Projectiles
{
    public class ProjectilesManager : UpdatableManager<BaseProjectile>
    {
        private void Start()
        {
            _updatableRemoved += DeleteProjectile;
        }

        public override void AddUpdatable(BaseProjectile projectile)
        {
            base.AddUpdatable(projectile);
            projectile.AddOnHitCallback(RemoveUpdatable);
        }

        public override void RemoveUpdatable(BaseProjectile projectile)
        {
            projectile.RemoveOnHitCallback(RemoveUpdatable);
            base.RemoveUpdatable(projectile);
        }

        private void DeleteProjectile(BaseProjectile projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}
