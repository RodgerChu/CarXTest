using System.Collections;
using System.Collections.Generic;
using TD.Monsters;
using UnityEngine;

namespace TD.Towers.Detectors
{
    public abstract class EnemyDetector: MonoBehaviour
    {
        public abstract List<BaseMonster> Monsters { get; }
    }
}
