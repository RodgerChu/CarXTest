using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Monsters
{
    public class MonstersManager : MonoBehaviour
    {
        private List<BaseMonster> _monsters = new List<BaseMonster>();

        private Action<BaseMonster> _onMonsterKilled;

        private void Update()
        {
            foreach (var monster in _monsters)
                monster.UpdateInternal();
        }

        public void AddMonster(BaseMonster monster)
        {
            _monsters.Add(monster);

            monster.AddOnDestionationReachedCallback(RemoveMonster);
            monster.AddOnKilledCallback(OnMonsterKilled);
        }

        public void RemoveMonster(BaseMonster monster)
        {
            _monsters.Remove(monster);

            monster.RemoveOnDestionReachedCallback(RemoveMonster);
            monster.RemoveOnKilledCallback(RemoveMonster);

            Destroy(monster.gameObject);
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
            RemoveMonster(monster);
        }
    }
}
