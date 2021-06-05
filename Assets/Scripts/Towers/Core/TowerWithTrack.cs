using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TD.Towers.Detectors;
using TD.Towers.AttackBehaviour;
using TD.Monsters;
using TD.Towers.TrackBehaviour;

namespace TD.Towers
{
    public class TowerWithTrack : BaseTower
    {
        [SerializeField] private BaseTrackBehaviour _trackBehaviour;

        protected override void InitializeShootCoditions()
        {
            base.InitializeShootCoditions();
            _shootConditions.Add(() => _trackBehaviour.CanShoot);
        }

        protected override void OnMonsterAppear(BaseMonster monster)
        {
            if (!_targetMonster)
            {
                _targetMonster = monster;
                _trackBehaviour.AlignToMonster(_targetMonster);
            }
        }

        protected override void OnMonsterDisappear(BaseMonster monster)
        {
            if (_targetMonster == monster)
            {
                _trackBehaviour.StopAligning();
                _targetMonster = _enemiesDetector.NextMonsterToAttack;              
                if (_targetMonster)
                    _trackBehaviour.AlignToMonster(_targetMonster);
            }
        }
    }
}
