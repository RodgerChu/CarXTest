using System.Collections;
using System.Collections.Generic;
using TD.Towers.Projectiles;
using UnityEngine;

namespace TD.Hitables
{
    public interface IHitable
    {
        void OnHit(BaseProjectile hitProjectile);
    }

}