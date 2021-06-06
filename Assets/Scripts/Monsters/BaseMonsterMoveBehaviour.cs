using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Monsters.Move
{
    public abstract class BaseMonsterMoveBehaviour : MonoBehaviour
    {
        protected Action _onDestinationReached;

        public abstract void Initialize(float speed);
        public abstract void MoveTo(Transform destination);
        public abstract void UpdateInternal();

        public void AddOnDestinationReachedCallback(Action callback)
        {
            _onDestinationReached += callback;
        }

        public void RemoveOnDestinationReachedCallback(Action callback)
        {
            _onDestinationReached -= callback;
        }
    }
}
