/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

namespace Unicorn.UI.States
{
    internal class OpenAnimationState: StateBase
    {
        public override void OnEnter(UIWindowFetus fetus, object arg1)
        {
            AssertTools.IsTrue(!fetus.isDelayedCloseWindow);
            var script = fetus.serializer.openWindowScript;
            var evt = fetus.serializer.onOpenWindowFinished;
            if (script is not null && evt is not null)
            {
                _openAnimation = new UIAnimation(script, evt);
                _isPlaying = _openAnimation.PlayAnimation(_OnOpenWindowFinishedCallback, this, fetus);
            }
            
            if (_isPlaying)
            {
                _playAnimationMask.OpenWindow();
            }
            else
            {
                fetus.ChangeState(StateKind.Opened);
            }
        }

        public override void OnExit(UIWindowFetus fetus, object arg1)
        {
            _openAnimation?.Release();

            if (_isPlaying)
            {
                _playAnimationMask.CloseWindow();
                _isPlaying = false;
            }
            
            AssertTools.IsTrue(!fetus.isDelayedCloseWindow);
        }

        private void _OnOpenWindowFinishedCallback(UIWindowFetus fetus)
        {
            _playAnimationMask.CloseWindow();
            _isPlaying = false;

            if (fetus.isDelayedCloseWindow)
            {
                fetus.isDelayedCloseWindow = false;
                fetus.ChangeState(StateKind.CloseAnimation);
            }
            else
            {
                fetus.ChangeState(StateKind.Opened);
            }
        }

        public override void OnOpenWindow(UIWindowFetus fetus)
        {
            fetus.isDelayedCloseWindow = false;
        }

        public override void OnCloseWindow(UIWindowFetus fetus)
        {
            fetus.isDelayedCloseWindow = true;
        }

        private UIAnimation _openAnimation;
        private bool _isPlaying;
    }
}