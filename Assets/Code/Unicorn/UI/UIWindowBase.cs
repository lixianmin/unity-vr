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


using System;
using System.Collections;
using Unicorn.UI.Internal;
using UnityEngine;

namespace Unicorn.UI
{
    public abstract partial class UIWindowBase
    {
        protected UIWindowBase()
        {
            _fetus = new WindowFetus(this);
        }

        // UI资源相关
        public abstract string GetResourcePath();
        public abstract Layout[] GetLayouts();
        public virtual RenderQueue GetRenderQueue() { return RenderQueue.Geometry; }
        
        // 加载事件相关
        public virtual void OnLoaded() {}
        public virtual void OnActivated() {}
        public virtual void OnOpened() {}
        public virtual void OnClosing() {}
        public virtual void OnDeactivating() {}
        public virtual void OnUnloading() {}

        // 逻辑帧: 大概10fps
        public virtual void LogicUpdate(float deltaTime) {}
        // 正常Update
        public virtual void Update(float deltaTime) {}

        internal virtual void Dispose()
        {
            if (_isReleased)
            {
                return;
            }

            _isReleased = true;
            UIManager._RemoveWindow(GetType());
            
            _fetus.Dispose();
            _fetus = null;
            _widgets.Clear();
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

        private WindowFetus _fetus;
        private Transform _transform;
        private  bool _isReleased;
    }
}