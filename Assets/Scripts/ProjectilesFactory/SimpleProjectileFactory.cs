using System.Collections;
using System.Collections.Generic;
using TD.Towers.Projectiles;
using UnityEngine;

namespace TD.Factories
{
    public class SimpleProjectileFactory : MonoBehaviour, IFactory<BaseProjectile>
    {
        [SerializeField] private BaseProjectile _prefab;

        public BaseProjectile Create()
        {
            return Instantiate(_prefab);
        }
    }
}
