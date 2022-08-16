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


namespace Unicorn.UI
{
    public abstract class UIWindowBase
    {
        protected UIWindowBase()
        {
            fetus = new UIWindowFetus
            {
                master = this,
                transform = null,
            };
        }
        
        public virtual RenderQueue RenderQueue => RenderQueue.Geometry;
        
        private UIWindowFetus fetus;
    }
}