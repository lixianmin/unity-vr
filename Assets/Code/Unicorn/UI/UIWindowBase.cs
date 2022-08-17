/********************************************************************
created:    2022-08-15
author:     lixianmin

1. life cycle of a window:

 -----------------------------------------------------------------
 |  new     --> load   --> active   --> animation --> open  -->  |
 |                                                            |  |
 |  release <-- unload <-- deactive <-- animation <-- close <--  |
 -----------------------------------------------------------------

Copyright (C) - All Rights Reserved
*********************************************************************/


using Unicorn.UI.Internal;
using UnityEngine;

namespace Unicorn.UI
{
    public abstract class UIWindowBase
    {
        protected UIWindowBase()
        {
            _fetus = new WindowFetus(this);
        }

        public abstract string GetResourcePath();
        public abstract Layout[] GetLayouts();
        
        public virtual void OnLoaded() {}
        public virtual void OnActivated() {}
        public virtual void OnOpened() {}
        public virtual void OnClosing() {}
        public virtual void OnDeactivating() {}
        public virtual void OnUnloading() {}

        public virtual void LogicUpdate(float deltaTime) {}
        public virtual void Update(float deltaTime) {}

        public virtual void Dispose()
        {
            if (_isReleased)
            {
                return;
            }

            _isReleased = true;
            UIManager._RemoveWindow(GetType());
            
            _fetus.Dispose();
            _fetus = null;
        }

        internal WindowFetus GetFetus()
        {
            return _fetus;
        }
        
        public Transform GetTransform()
        {
            return _transform;
        }

        internal void _SetTransform(Transform transform)
        {
            _transform = transform;
        }

        public virtual RenderQueue GetRenderQueue()
        {
            return RenderQueue.Geometry;
        }
        
        private WindowFetus _fetus;
        private Transform _transform;
        private  bool _isReleased;
    }
}