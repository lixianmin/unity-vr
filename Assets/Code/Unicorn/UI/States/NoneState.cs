/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/
namespace Unicorn.UI.States
{
    internal class NoneState : StateBase
    {
        public override void OnEnter(UIWindowFetus fetus, object arg1)
        {
            
        }

        public override void OnExit(UIWindowFetus fetus, object arg1)
        {
            
        }

        public void OnOpenWindow(UIWindowFetus fetus)
        {
            fetus.ChangeState(StateKind.Load);
        }
        
        public void Release()
        {
            
        }
    }
}