using UnityEngine;

namespace TD.Configs.Projectiles
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/ProjectileConfig", order = 2)]
    public class ProjectileConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;

        public float Speed => _speed;
        public float Damage => _damage;
    }
}