using System.Collections;
using System.Collections.Generic;
using TD.Monsters;
using UnityEngine;

namespace TD.Towers.TrackBehaviour
{
    public class BallistickTrackBehaviour : BaseTrackBehaviour
    {
        [SerializeField] private float _rotationSpeed = 3f;
        [SerializeField] private float _treshhold = 0.2f;

        public override bool CanShoot => _canShoot;

        private float _sqrTreshhold = 0f;
        private bool _canShoot = false;
        private BaseMonster _alignTarget;
        private Coroutine _alignCoroutine;

        private void Awake()
        {
            _sqrTreshhold = _treshhold * _treshhold;
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
            
            while(true)
            {
                var projectileStartingVelocitySqr = (_towerBarrel.transform.forward * _expectedProjectiles.Speed).sqrMagnitude;
                Vector3 shootPoint;
                Vector3 targetPosition;

                var timeToHitTarget = GetTimeForProjectileToHit(
                    _alignTarget.transform.position, 
                    _alignTarget.transform.forward * _alignTarget.Speed,
                    projectileStartingVelocitySqr);

                targetPosition = _alignTarget.transform.position + _alignTarget.transform.forward * _alignTarget.Speed * timeToHitTarget;

                shootPoint = GetShootingPoint(
                    _alignTarget.transform.position,
                    _alignTarget.transform.forward * _alignTarget.Speed,
                    timeToHitTarget);

                var fireTreshholdSqr = (shootPoint - targetPosition).sqrMagnitude;
                while (fireTreshholdSqr <= _sqrTreshhold)
                {
                    timeToHitTarget = GetTimeForProjectileToHit(
                    _alignTarget.transform.position,
                    _alignTarget.transform.forward * _alignTarget.Speed,
                    projectileStartingVelocitySqr);

                    targetPosition = _alignTarget.transform.position + _alignTarget.transform.forward * _alignTarget.Speed * timeToHitTarget;

                    shootPoint = GetShootingPoint(
                        _alignTarget.transform.position,
                        _alignTarget.transform.forward * _alignTarget.Speed,
                        timeToHitTarget);
                }

                var rotation = GetShootingRotation(_towerBarrel.position, _alignTarget.transform.position, _expectedProjectiles.Speed);

                _towerBarrel.transform.rotation = Quaternion.Slerp(_towerBarrel.transform.rotation, rotation, Time.deltaTime * _rotationSpeed);

                var currentThreshhold = Quaternion.Angle(_towerBarrel.transform.rotation, rotation);
                _canShoot = currentThreshhold <= _treshhold;

                yield return null;
            }
        }

        private bool CalculateShootingAngle(Vector3 muzzlePos, Vector3 targetPos, float projectileVelocity, out float angle)
        {
            Vector3 dir = targetPos - muzzlePos;
            float vSqr = projectileVelocity * projectileVelocity;
            float y = dir.y;
            dir.y = 0.0f;
            float x = dir.sqrMagnitude;
            float g = -Physics.gravity.y;

            float uRoot = vSqr * vSqr - g * (g * (x) + (2.0f * y * vSqr));

            if (uRoot < 0.0f)
            {
                angle = -45.0f;
                return false;
            }

            angle = -Mathf.Atan2(g * Mathf.Sqrt(x), vSqr + Mathf.Sqrt(uRoot)) * Mathf.Rad2Deg;
            return true;
        }


        private float GetTimeForProjectileToHit(Vector3 targetPos, Vector3 targetVelocity, float projectileVelocitySqr)
        {
            float a = projectileVelocitySqr - (targetVelocity.z * targetVelocity.z + targetVelocity.y * targetVelocity.y);
            float b = targetPos.x * targetVelocity.z + targetPos.y * targetVelocity.y;
            float c = targetPos.x * targetPos.x + targetPos.y * targetPos.y;

            float d = b * b + a * c;

            float t = 0;
            if (d >= 0)
            {
                t = (b + Mathf.Sqrt(d)) / a;
                if (t < 0)
                    t = 0;
            }

            return t;
        }

        private Vector3 GetShootingPoint(Vector3 targetPos, Vector3 targetVelocity, float time)
        {
            return targetPos + targetVelocity * time + (-Physics.gravity * time * time) / 2;
        }

        private Quaternion GetShootingRotation(Vector3 muzzlePos, Vector3 targetPos, float projectileVelocity)
        {
            float angle;
            CalculateShootingAngle(muzzlePos, targetPos, projectileVelocity, out angle);


            Vector3 wantedRotationVector = Quaternion.LookRotation(targetPos - muzzlePos).eulerAngles;
            wantedRotationVector.x = angle;
            return Quaternion.Euler(wantedRotationVector);
        }
    }
}
