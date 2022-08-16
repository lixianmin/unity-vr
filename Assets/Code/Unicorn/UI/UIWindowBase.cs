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

        public virtual void OnLoaded() { }
        public virtual void Release() { }

        public abstract string ResourcePath { get;  }

        public void _SetTransform(Transform transform)
        {
            _transform = transform;
        }

        public virtual RenderQueue RenderQueue => RenderQueue.Geometry;
        
        private UIWindowFetus _fetus;
        private Transform _transform;
    }
}