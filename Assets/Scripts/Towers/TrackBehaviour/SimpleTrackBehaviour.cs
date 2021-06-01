using TD.Monsters;
using UnityEngine;

namespace TD.Towers.TrackBehaviour
{
    public class SimpleTrackBehaviour : BaseTrackBehaviour
    {
        [SerializeField] private float _rotationSpeed = 3f;
        [SerializeField] private float _treshhold = 0.2f;

        public override bool CanShoot => _canShoot;

        private bool _canShoot = false;
        private BaseMonster _alightTarget;

        public override void AlignToMonster(BaseMonster target)
        {
            _alightTarget = target;
        }

        public override void StopAligning()
        {
            _alightTarget = null;
        }

        private Vector3 FindTrackPoint()
        {
            var currentDistance = Vector3.Distance(_alightTarget.transform.position, transform.position);
            var result = Vector3.zero;
            while(currentDistance > _treshhold)
            {
                var rotationDirection = transform.forward - _alightTarget.transform.position;
            }
        }
    }
}
