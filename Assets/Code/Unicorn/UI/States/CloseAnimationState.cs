/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn.UI.Internal;

namespace Unicorn.UI.States
{
    internal class CloseAnimationState : StateBase
    {
        public override void OnEnter(WindowFetus fetus, object arg1)
        {
            if (fetus.isDelayedOpenWindow)
            {
                fetus.isDelayedOpenWindow = false;
                fetus.ChangeState(StateKind.Opened);
                return;
            }

            var script = fetus.serializer.closeWindowScript;
            var evt = fetus.serializer.onCloseWindowFinished;
            if (script is not null && evt is not null)
            {
                _closeAnimation = new UIAnimation(script, evt);
                _isPlaying = _closeAnimation.PlayAnimation(_OnCloseWindowFinishedCallback, fetus);
            }

            if (_isPlaying)
            {
                _playAnimationMask.OpenWindow();
            }
            else
            {
                fetus.ChangeState(StateKind.Closed);
            }
        }

        public override void OnExit(WindowFetus fetus, object arg1)
        {
            _closeAnimation?.Dispose();
            _closeAnimation = null;

            if (_isPlaying)
            {
                _playAnimationMask.CloseWindow();
                _isPlaying = false;
            }
        }

        private void _OnCloseWindowFinishedCallback(WindowFetus fetus)
        {
            _playAnimationMask.CloseWindow();
            _isPlaying = false;

            if (fetus.isDelayedOpenWindow)
            {
                fetus.isDelayedOpenWindow = false;
                fetus.ChangeState(StateKind.OpenAnimation);
            }
            else
            {
                fetus.ChangeState(StateKind.Closed);
            }
        }

        public override void OnOpenWindow(WindowFetus fetus)
        {
            fetus.isDelayedOpenWindow = true;
        }

        public override void OnCloseWindow(WindowFetus fetus)
        {
            fetus.isDelayedOpenWindow = false;
        }

        private UIAnimation _closeAnimation;
        private bool _isPlaying;
    }
}