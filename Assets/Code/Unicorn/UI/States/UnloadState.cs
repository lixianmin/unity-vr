/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/
namespace Unicorn.UI.States
{
    internal class UnloadState : StateBase
    {
        public override void OnEnter(UIWindowFetus fetus, object arg1)
        {
            AssertTools.IsTrue(!fetus.isDelayedOpenWindow);
            var master = fetus.master;
            master.OnUnloading();

            fetus.isLoaded = false;
            fetus.ChangeState(StateKind.None);
            master.Dispose();

            if (fetus.isDelayedOpenWindow)
            {
                fetus.isDelayedOpenWindow = false;
                // UIManager.OpenWindow();
            }
            
            AssertTools.IsTrue(!fetus.isDelayedOpenWindow);
        }

        public override void OnOpenWindow(UIWindowFetus fetus)
        {
            fetus.isDelayedOpenWindow = true;
        }

        public override void OnCloseWindow(UIWindowFetus fetus)
        {
            fetus.isDelayedOpenWindow = false;
        }
    }
}