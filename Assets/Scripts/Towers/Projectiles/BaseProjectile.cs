using System;
using System.Collections;
using System.Collections.Generic;
using TD.Configs.Projectiles;
using UnityEngine;

namespace TD.Towers.Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] private ProjectileConfig _projectileConfig;

        public float ProjectileSpeed => _projectileConfig.Speed;
        public float ProjectileDamage => _projectileConfig.Damage;

        protected Action<BaseProjectile> OnHit;

        public abstract void UpdateInternal();

        public virtual void SetDirection(Transform direction)
        {
            transform.LookAt(direction);
        }

        public virtual void AddOnHitCallback(Action<BaseProjectile> callback)
        {
            OnHit += callback;
        }

        public virtual void RemoveOnHitCallback(Action<BaseProjectile> callback)
        {
            OnHit -= callback;
        }
    }
}