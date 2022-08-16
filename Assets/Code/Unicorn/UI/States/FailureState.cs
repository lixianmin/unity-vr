/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

namespace Unicorn.UI.States
{
    internal class FailureState : StateBase
    {
        public override void OnEnter(UIWindowFetus fetus, object failureText)
        {
            Console.Error.WriteLine("Enter FailureState, failureText={0}", failureText);
            fetus.ChangeState(StateKind.None);
            fetus.master.Release();
        }

        public override void OnExit(UIWindowFetus fetus, object arg1)
        {
            
        }
    }
}