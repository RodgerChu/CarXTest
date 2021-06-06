using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Monsters.Move
{
    public class SimpleMonsterMoveBehaviour : BaseMonsterMoveBehaviour
    {
        [SerializeField] private float _destinationReachedTreshhold = 0.2f;

        private Transform _destination;
        private float _speed = 0f;

        public override void Initialize(float speed)
        {
            _speed = speed;
        }

        public override void MoveTo(Transform destination)
        {
            _destination = destination;
        }

        public override void UpdateInternal()
        {
            if (_destination == null)
                return;

            if (Vector3.Distance(transform.position, _destination.position) <= _destinationReachedTreshhold)
            {
                _onDestinationReached?.Invoke();
                _destination = null;
                return;
            }

            var translation = _destination.position - transform.position;
            if (translation.magnitude > _speed)
            {
                translation = translation.normalized * _speed * Time.deltaTime;
            }

            transform.Translate(translation);
        }
    }
}
