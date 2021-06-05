using System.Collections;
using System.Collections.Generic;
using TD.Monsters;
using UnityEngine;

namespace TD.Towers.TrackBehaviour
{
    public class CannonTrackBehaviour : BaseTrackBehaviour
    {
        [SerializeField] private float _rotationSpeed = 3f;
        [SerializeField] private float _treshhold = 0.2f;
        [SerializeField] private int _maxIterationsPerFrame = 5;
        [SerializeField] private Transform _shootPoint;

        public override bool CanShoot => _canShoot;

        private float _sqrTreshhold = 0f;
        private bool _canShoot = false;
        private BaseMonster _alignTarget;
        private Coroutine _alignCoroutine;

        private float _projectileVelocitySqr;
        private float _projectileVelocity;
        private Vector3 _shootPosition;

        private void Awake()
        {
            _sqrTreshhold = _treshhold * _treshhold;

            _projectileVelocitySqr = (_towerBarrel.transform.forward * _expectedProjectiles.Speed).sqrMagnitude;
            _projectileVelocity = Mathf.Sqrt(_projectileVelocitySqr);
            _shootPosition = _shootPoint.position;
        }

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
            while (true)
            {
                float fireTreshholdSqr = 0;

                var shootingAngle = CalculateShootingAngle(_shootPosition, _alignTarget.transform.position, _projectileVelocitySqr * _towerBarrel.transform.forward.sqrMagnitude);
                var timeToHit = GetTimeOfFlight(_projectileVelocity, shootingAngle);
                var targetNewPosition = _alignTarget.transform.position + _alignTarget.transform.forward * _alignTarget.Speed * timeToHit;

                fireTreshholdSqr = (_alignTarget.transform.position - targetNewPosition).sqrMagnitude;

                shootingAngle = CalculateShootingAngle(
                        _shootPosition,
                        targetNewPosition,
                        _projectileVelocitySqr);

                for (var iteration = 1; iteration <= _maxIterationsPerFrame; iteration++)
                {
                    var targetPosOnIterationStart = targetNewPosition;
                    timeToHit = GetTimeOfFlight(_projectileVelocity, Mathf.Abs(shootingAngle));

                    targetNewPosition = _alignTarget.transform.position + _alignTarget.transform.forward * _alignTarget.Speed * timeToHit;

                    shootingAngle = CalculateShootingAngle(
                        _shootPosition,
                        targetNewPosition,
                        _projectileVelocitySqr);

                    fireTreshholdSqr = (targetNewPosition - targetPosOnIterationStart).sqrMagnitude;                    
                }

                var rotation = GetAimRotation(_towerBarrel.position, targetNewPosition, shootingAngle);
                _towerBarrel.transform.rotation = Quaternion.Lerp(_towerBarrel.transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
                var currentThreshhold = Quaternion.Angle(_towerBarrel.transform.rotation, rotation);
                _canShoot = fireTreshholdSqr <= _sqrTreshhold;
                
                yield return null;
            }
        }

        private float CalculateShootingAngle(Vector3 muzzlePos, Vector3 targetPos, float projectileVelocitySqr)
        {
            Vector3 direction = targetPos - muzzlePos;
            float y = direction.y;
            float x = direction.sqrMagnitude;
            float g = -Physics.gravity.y;
            float angle;

            float uRoot = projectileVelocitySqr * projectileVelocitySqr - g * (g * (x) + (2.0f * y * projectileVelocitySqr));

            if (uRoot < 0.0f)            
                angle = -45.0f;            
            else
                angle = -Mathf.Atan2(g * Mathf.Sqrt(x), projectileVelocitySqr + Mathf.Sqrt(uRoot)) * Mathf.Rad2Deg;

            return angle;
        }

        private Vector3 GetShootingPoint(Vector3 targetPos, Vector3 targetVelocity, float time)
        {
            return targetPos + targetVelocity * time + (-Physics.gravity * time * time) / 2;
        }

        private float GetTimeOfFlight(float vel, float angle)
        {
            return 2.0f * vel * Mathf.Sin(angle) / -Physics.gravity.y;
        }

        private Quaternion GetAimRotation(Vector3 muzzlePos, Vector3 targetPos, float angle)
        {
            Vector3 wantedRotationVector = Quaternion.LookRotation(targetPos - muzzlePos).eulerAngles;
            wantedRotationVector.x += angle;
            return Quaternion.Euler(wantedRotationVector);
        }
    }
}
