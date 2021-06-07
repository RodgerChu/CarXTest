using System;
using System.Collections;
using System.Collections.Generic;
using TD.Core;
using UnityEngine;

namespace TD.Managers
{
    public abstract class UpdatableManager<T>: MonoBehaviour where T: IUpdatable
    {
        private List<T> _updatables = new List<T>();
        private List<T> _newUpdatables = new List<T>();
        private List<T> _updatablesToRemove = new List<T>();
        private bool _isUpdating = false;

        protected Action<T> _updatableRemoved;

        protected virtual void Update()
        {
            if (_newUpdatables.Count != 0)
                AddUpdatables();
            if (_updatablesToRemove.Count != 0)
                RemoveUpdatables();

            _isUpdating = true;

            foreach (var updatable in _updatables)
                updatable.UpdateInternal();

            _isUpdating = false;
        }

        public virtual void AddUpdatable(T updatable)
        {
            if (_isUpdating)
                _newUpdatables.Add(updatable);
            else
                _updatables.Add(updatable);
        }

        public virtual void RemoveUpdatable(T updatable)
        {
            if (_isUpdating)
                _updatablesToRemove.Add(updatable);
            else
            {
                _updatables.Remove(updatable);
                _updatableRemoved?.Invoke(updatable);
            }
        }

        protected virtual void RemoveUpdatables()
        {
            foreach (var upd in _updatablesToRemove)
            {
                _updatables.Remove(upd);
                _updatableRemoved?.Invoke(upd);
            }
            _updatablesToRemove.Clear();
        }

        protected virtual void AddUpdatables()
        {
            foreach (var upd in _newUpdatables)
                _updatables.Add(upd);

            _newUpdatables.Clear();
        }
    }
}
