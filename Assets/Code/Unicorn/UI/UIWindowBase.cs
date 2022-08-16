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


using UnityEngine;

namespace Unicorn.UI
{
    public abstract class UIWindowBase
    {
        protected UIWindowBase()
        {
            _fetus = new UIWindowFetus
            {
                master = this,
                transform = null,
            };
        }

        public virtual void OnLoaded() {}
        public virtual void OnActivated() {}
        public virtual void OnOpened() {}
        public virtual void OnClosing() {}
        public virtual void OnDeactivating() {}
        public virtual void OnUnloading() {}

        public virtual void LogicUpdate(float deltaTime) {}
        public virtual void Update(float deltaTime) {}
        public virtual void Dispose() {}

        public abstract string GetResourcePath();

        
        internal UIWindowFetus GetFetus()
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
        
        private UIWindowFetus _fetus;
        private Transform _transform;
    }
}