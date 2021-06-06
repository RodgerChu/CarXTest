using System.Collections;
using System.Collections.Generic;
using TD.Configs.Monsters;
using UnityEngine;

namespace TD.Monsters.Health
{
    public class SimpleMonsterHealthBehaviour : BaseMonsterHealthBehaviour
    {
        private float _currentHp = 0f;

        public override void Initialize(MonsterStats stats)
        {
            _currentHp = stats.MaxHP;
        }

        public override void TakeDamage(float damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
                _onKilledCallback?.Invoke();
        }
    }
}
