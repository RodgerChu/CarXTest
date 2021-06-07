using System;
using System.Collections;
using System.Collections.Generic;
using TD.Configs.Projectiles;
using TD.Core;
using TD.Hitables;
using UnityEngine;

namespace TD.Towers.Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour, IUpdatable
    {
        [SerializeField] private ProjectileConfig _projectileConfig;

        public float ProjectileSpeed => _projectileConfig.Speed;
        public float ProjectileDamage => _projectileConfig.Damage;

        protected Action<BaseProjectile> OnHit;

        public abstract void UpdateInternal();

        public virtual void SetDirection(Vector3 direction)
        {
            gameObject.transform.LookAt(direction);
        }

        public virtual void AddOnHitCallback(Action<BaseProjectile> callback)
        {
            OnHit += callback;
        }

        public virtual void RemoveOnHitCallback(Action<BaseProjectile> callback)
        {
            OnHit -= callback;
        }

        private void OnTriggerEnter(Collider other)
        {
            var hitable = other.GetComponent<IHitable>();
            if (hitable != null)
            {
                Debug.LogError("removing pro");
                hitable.OnHit(this);
                OnHit?.Invoke(this);
            }
        }
    }
}