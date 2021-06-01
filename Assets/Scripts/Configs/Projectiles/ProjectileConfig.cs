using UnityEngine;

namespace TD.Configs.Projectiles
{
    public class ProjectileConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;

        public float Speed => _speed;
        public float Damage => _damage;
    }
}