/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn.UI.Internal;

namespace Unicorn.UI.States
{
    internal class ClosedState : StateBase
    {
        private static bool _IsWindowCacheable(WindowFetus fetus)
        {
            // return fetus._master.CACHE_HINT and LuaUITools.isBigMemoryMode
            return false;
        }
        
        public override void OnEnter(WindowFetus fetus, object failureText)
        {
            if (_IsWindowCacheable(fetus))
            {
                fetus.gameObject.SetActiveEx(false);
                fetus.isWindowCached = true;
            }
            else
            {
                fetus.ChangeState(StateKind.Unload);
            }
        }

        public override void OnOpenWindow(WindowFetus fetus)
        {
            if (fetus.isWindowCached)
            {
                fetus.gameObject.SetActiveEx(true);
                fetus.isWindowCached = false;
            }
            
            fetus.ChangeState(StateKind.OpenAnimation);
        }
    }
}