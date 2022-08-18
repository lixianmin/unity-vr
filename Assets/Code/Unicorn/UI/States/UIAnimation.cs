/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using System.Collections;
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
            _onAnimationFinished = onAnimationFinished;
            
            _AddListener_OnFinished();
        }
        
        private void _AddListener_OnFinished()
        {
            _onFinishHandler = () =>
            {
                _StopAnimationRoutine();
                _Callback();
            };
                
            _onAnimationFinished.AddListener(_onFinishHandler);
        }

        public void Dispose()
        {
            if (_onFinishHandler is not null)
            {
                _onAnimationFinished.RemoveListener(_onFinishHandler);
                _onFinishHandler = null;
            }
            
            _StopAnimationRoutine();

            _animationScript = null;
            _onFinishedCallback = null;
            _fetus = null;
        }

        public bool PlayAnimation(Action<WindowFetus> onFinishedCallback, WindowFetus fetus)
        {
            if (_animationScript is null || onFinishedCallback == null)
            {
                return false;
            }

            _onFinishedCallback = onFinishedCallback;
            _fetus = fetus;
            _StopAnimationRoutine();
            _animationScript.enabled = true;
            _animationScript.enabled = false;
            CoroutineManager.StartCoroutine(_CoWaitAnimationDone(), out _animationRoutine);
            
            return true;
        }

        private IEnumerator _CoWaitAnimationDone()
        {
            var breakTime = Time.time + _maxAnimationTime;
            while (Time.time < breakTime)
            {
                yield return null;
            }

            _animationRoutine = null;
            _Callback();
        }

        private void _Callback()
        {
            _onFinishedCallback?.Invoke( _fetus);
        }
        
        private void _StopAnimationRoutine()
        {
            _animationRoutine?.Kill();
        }
        
        private MonoBehaviour _animationScript;
        private UnityEvent _onAnimationFinished;

        private float _maxAnimationTime;
        private CoroutineItem _animationRoutine;
        private UnityAction _onFinishHandler;
        private Action<WindowFetus> _onFinishedCallback;
        private StateBase _state;
        private WindowFetus _fetus;
    }
}