using System.Collections;
using System.Collections.Generic;
using TD.Monsters;
using UnityEngine;

namespace TD.Towers.Detectors
{
    public class SimpleEnemiesDetector: EnemyDetector
    {
        public override List<BaseMonster> Monsters => _monsters;
        private List<BaseMonster> _monsters = new List<BaseMonster>();

        private void OnTriggerEnter(Collider other)
        {
            var monster = other.gameObject.GetComponent<BaseMonster>();
            if (monster)
            {
                _monsters.Add(monster);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var monster = other.gameObject.GetComponent<BaseMonster>();
            if (monster)
            {
                _monsters.Remove(monster);
            }
        }
    }
}
