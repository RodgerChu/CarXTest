using System;
using System.Collections;
using System.Collections.Generic;
using TD.Configs.Monsters;
using UnityEngine;

namespace TD.Monsters.Health
{
    public abstract class BaseMonsterHealthBehaviour : MonoBehaviour
    {       
        protected Action _onKilledCallback;

        public abstract void Initialize(MonsterStats stats);

        public abstract void TakeDamage(float damage);

        public void AddOnKilledCallback(Action callback)
        {
            _onKilledCallback += callback;
        }

        public void RemoveOnKilledCallback(Action callback)
        {
            _onKilledCallback -= callback;
        }
    }
}