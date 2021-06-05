using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Towers.Projectiles
{
    public class SimpleProjectile : BaseProjectile
    {
        public override void UpdateInternal()
        {
            var oldPos = gameObject.transform.position;
            oldPos += gameObject.transform.forward * ProjectileSpeed * Time.deltaTime;
            gameObject.transform.position = oldPos;
        }
    }
}
