/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using Unicorn.UI.Internal;
using UnityEngine;
using UnityEngine.Events;

namespace Unicorn.UI.States
{
    internal class UIAnimation
    {
        public UIAnimation(MonoBehaviour animationScript, UnityEvent onAnimationFinished)
        {
            if (animationScript is null || onAnimationFinished is null)
            {
                return;
            }

            _animationScript = animationScript;
            // todo 这里不是单纯的animation script, 这里应该是某个tween
        }

        public bool PlayAnimation(Action<WindowFetus> onFinishedCallback, StateBase state, WindowFetus fetus)
        {
            if (_animationScript is null || onFinishedCallback == null)
            {
                return false;
            }

            return true;
        }

        private void _AddListener_OnFinished(MonoBehaviour animationScript)
        {
            
        }

        public void Release()
        {
            
        }

        private MonoBehaviour _animationScript;
        private UnityEvent _onAnimationFinished;
    }
}