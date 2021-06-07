using System;
using System.Collections;
using System.Collections.Generic;
using TD.Managers;
using UnityEngine;

namespace TD.Monsters
{
    public class MonstersManager : UpdatableManager<BaseMonster>
    {
        private Action<BaseMonster> _onMonsterKilled;

        private void Start()
        {
            _updatableRemoved += DeleteMonster;
        }

        public override void AddUpdatable(BaseMonster monster)
        {
            Debug.LogError($"Adding monster {monster} | {monster.gameObject.name}");
            base.AddUpdatable(monster);

            monster.AddOnDestionationReachedCallback(RemoveUpdatable);
            monster.AddOnKilledCallback(OnMonsterKilled);
        }

        public override void RemoveUpdatable(BaseMonster monster)
        {
            base.RemoveUpdatable(monster);

            monster.RemoveOnDestionReachedCallback(RemoveUpdatable);
            monster.RemoveOnKilledCallback(RemoveUpdatable);
        }

        public void AddOnMonsterKilledCallback(Action<BaseMonster> callback)
        {
            _onMonsterKilled += callback;
        }

        public void RemoveOnMonsterKilledCallback(Action<BaseMonster> callback)
        {
            _onMonsterKilled -= callback;
        }

        private void OnMonsterKilled(BaseMonster monster)
        {
            _onMonsterKilled?.Invoke(monster);
            RemoveUpdatable(monster);
        }

        private void DeleteMonster(BaseMonster monster)
        {
            Destroy(monster.gameObject);
        }
    }
}
