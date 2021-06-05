using System.Collections;
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
        private BaseMonster _alignTarget;

        private Coroutine _alignCoroutine;

        public override void AlignToMonster(BaseMonster target)
        {          
            if (_alignCoroutine != null)
                StopAligning();

            _alignTarget = target;
            _alignCoroutine = StartCoroutine(AlignCoroutine());
        }

        public override void StopAligning()
        {
            StopCoroutine(_alignCoroutine);
            _alignTarget = null;            
            _canShoot = false;
        }

        private IEnumerator AlignCoroutine()
        {
            while(true)
            {
                var shootPoint = GetPointToShoot(_alignTarget.transform.position, _alignTarget.gameObject.transform.forward * _alignTarget.Speed, _expectedProjectiles.Speed);
                var rotationDirection = (shootPoint - transform.position).normalized;
                var rotationValue = Quaternion.LookRotation(rotationDirection);
                _towerBarrel.transform.rotation = Quaternion.Slerp(_towerBarrel.transform.rotation, rotationValue, Time.deltaTime * _rotationSpeed);

                var currentThreshhold = Quaternion.Angle(_towerBarrel.transform.rotation, rotationValue);
                _canShoot = currentThreshhold <= _rotationSpeed;

                yield return null;
            }
        }

        private Vector3 GetPointToShoot(Vector3 targetPosition, Vector3 targetVelocity, float projectileSpeed)
        {
            var towerPosition = _towerBarrel.transform.position;
            Vector3 displacement = targetPosition - towerPosition;
            float targetMoveAngle = Vector3.Angle(-displacement, targetVelocity) * Mathf.Deg2Rad;

            if (targetVelocity.magnitude == 0 || targetVelocity.magnitude > projectileSpeed && Mathf.Sin(targetMoveAngle) / projectileSpeed > Mathf.Cos(targetMoveAngle) / targetVelocity.magnitude)
            {
                Debug.LogError("Position prediction is not feasible. Check projectile and/or monster settings and thier speeds");
                return Vector3.zero;
            }

            float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / projectileSpeed);
            return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
        }
    }
}
