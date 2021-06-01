using System.Collections;
using System.Collections.Generic;
using TD.Configs.Projectiles;
using TD.Monsters;
using UnityEngine;

namespace TD.Towers.TrackBehaviour
{
    public abstract class BaseTrackBehaviour : MonoBehaviour
    {
        [SerializeField] protected ProjectileConfig _expectedProjectiles;
        [SerializeField] protected Transform _towerBarrel;

        public abstract bool CanShoot { get; }
        public abstract void AlignToMonster(BaseMonster target);
        public abstract void StopAligning();
    }
}
