using System.Collections.Generic;
using UnityEngine;

namespace TD.Towers.Projectiles
{
    public class ProjectilesManager : MonoBehaviour
    {
        private List<BaseProjectile> _projectiles = new List<BaseProjectile>();

        private void Update()
        {
            foreach (var projectile in _projectiles)
                projectile.UpdateInternal();
        }

        public void AddProjectile(BaseProjectile projectile)
        {
            _projectiles.Add(projectile);
            projectile.AddOnHitCallback(RemoveProjectile);
        }

        public void RemoveProjectile(BaseProjectile projectile)
        {
            _projectiles.Remove(projectile);
            projectile.RemoveOnHitCallback(RemoveProjectile);
        }
    }
}
