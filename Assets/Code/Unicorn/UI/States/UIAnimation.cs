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
            
            _onFinishHandler = () =>
            {
                _StopAnimationRoutine();
                _onFinishedCallback?.Invoke();
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
        }

        public bool PlayAnimation(Action onFinishedCallback)
        {
            if (_animationScript is null || onFinishedCallback is null)
            {
                return false;
            }

            _onFinishedCallback = onFinishedCallback;
            _StopAnimationRoutine();
            
            _animationScript.enabled = true;
            _animationScript.enabled = false;
            CoroutineManager.StartCoroutine(_CoWaitAnimationDone(), out _animationRoutine);
            
            return true;
        }

        private IEnumerator _CoWaitAnimationDone()
        {
            const float maxTime = 2.0f;
            var breakTime = Time.time + maxTime;
            while (Time.time < breakTime)
            {
                yield return null;
            }

            _animationRoutine = null;
            _onFinishedCallback?.Invoke();
        }

        private void _StopAnimationRoutine()
        {
            _animationRoutine?.Kill();
        }
        
        private MonoBehaviour _animationScript;
        private UnityEvent _onAnimationFinished;

        private CoroutineItem _animationRoutine;
        private UnityAction _onFinishHandler;
        private Action _onFinishedCallback;
    }
}