/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

namespace Unicorn.UI.States
{
    internal class OpenedState : StateBase
    {
        public override void OnEnter(UIWindowFetus fetus, object arg1)
        {
            AssertTools.IsTrue(!fetus.isDelayedCloseWindow);
            var master = fetus.master;
            master.OnOpened();
            UIManager._SetForegroundWindow(master);
            fetus.isOpened = true;
        }

        public override void OnExit(UIWindowFetus fetus, object arg1)
        {
            var master = fetus.master;
            // isOpened is used to judge whether the window can be activated, thus it must be set to be false as soon as OnExit() is called.
            fetus.isOpened = false;
           
            UIManager._OnClosingWindow(master);
            master.OnClosing();
        }

        public override void OnOpenWindow(UIWindowFetus fetus)
        {
            if (fetus.isOpened)
            {
                var master = fetus.master;
                UIManager._SetForegroundWindow(master);
            }
            else
            { // If the same window is opened again in OnClosing() or _onBeforeOpened
                fetus.isDelayedOpenWindow = true;
            }
        }

        public override  void OnCloseWindow(UIWindowFetus fetus)
        {
            fetus.isDelayedOpenWindow = false;
            if (fetus.isOpened)
            {
                fetus.ChangeState(StateKind.CloseAnimation);
            }
        }
    }
}