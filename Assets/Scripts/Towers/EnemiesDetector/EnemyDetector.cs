using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TD.Monsters;
using UnityEngine;

namespace TD.Towers.Detectors
{
    public abstract class EnemyDetector: MonoBehaviour
    {
        public abstract List<BaseMonster> Monsters { get; }

        protected Action<BaseMonster> _onMonsterAppearCallback;
        protected Action<BaseMonster> _onMonsterDisappearCallback;

        public virtual BaseMonster NextMonsterToAttack => Monsters.FirstOrDefault();

        public void AddOnMonsterAppearCallback(Action<BaseMonster> callback)
        {
            _onMonsterAppearCallback += callback;
        }

        public void RemoveOnMonsterAppearCallback(Action<BaseMonster> callback)
        {
            _onMonsterAppearCallback -= callback;
        }

        public void AddOnMonsterDisappearCallback(Action<BaseMonster> callback)
        {
            _onMonsterDisappearCallback += callback;
        }

        public void RemoveOnMonsterDisappearCallback(Action<BaseMonster> callback)
        {
            _onMonsterDisappearCallback -= callback;
        }
    }
}
