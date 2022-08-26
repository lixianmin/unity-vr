/********************************************************************
created:    2022-08-26
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using UnityEngine.Events;

namespace Unicorn
{
    public class ScriptBase
    {
        public virtual void Awake() { }
        public virtual void OnDestroy() { }

        public void AddListener(UnityEvent evt, UnityAction handler)
        {
            _listener.AddListener(evt, handler);
        }
        
        public void AddListener<T>(UnityEvent<T> evt, UnityAction<T> handler)
        {
            _listener.AddListener(evt, handler);
        }

        internal void RemoveAllListeners()
        {
            _listener.RemoveAllListeners();
        }
        
        public UnityEngine.Object[] GetTargets()
        {
            return _targets;
        }
        
        internal void _SetTargets(UnityEngine.Object[] targets)
        {
            _targets = targets;
        }
        
        private UnityEngine.Object[] _targets;
        private readonly EventListener _listener = new();
    }
}