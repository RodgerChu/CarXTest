using System.Collections;
using System.Collections.Generic;
using TD.Configs.Projectiles;
using UnityEngine;

namespace TD.Towers.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsDrivenProjectile : BaseProjectile
    {
        private Vector3 _startVelocity => transform.forward * ProjectileSpeed;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public override void SetDirection(Vector3 direction)
        {
            base.SetDirection(direction);

            _rb.AddForce(_startVelocity, ForceMode.Impulse);
        }

        public override void UpdateInternal()
        {
        }
    }
}
