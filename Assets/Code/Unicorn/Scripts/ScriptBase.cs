/********************************************************************
created:    2022-08-26
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

namespace Unicorn.Scripts
{
    public class ScriptBase
    {
        public virtual void Awake() { }
        public virtual void OnDestroy() { }

        public UnityEngine.Object[] GetTargets()
        {
            return _targets;
        }
        
        internal void _SetTargets(UnityEngine.Object[] targets)
        {
            _targets = targets;
        }
        
        private UnityEngine.Object[] _targets;
    }
}