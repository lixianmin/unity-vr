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
        public UIAnimation(UnityEvent onAnimationFinished)
        {
            if (onAnimationFinished is null)
            {
                return;
            }

            _onAnimationFinished = onAnimationFinished;
            
            _onAnimationFinishedHandler = () =>
            {
                _StopAnimationRoutine();
                _onFinishedCallback?.Invoke();
            };
                
            _onAnimationFinished.AddListener(_onAnimationFinishedHandler);
        }

        public void Dispose()
        {
            if (_onAnimationFinishedHandler is not null)
            {
                _onAnimationFinished.RemoveListener(_onAnimationFinishedHandler);
                _onAnimationFinishedHandler = null;
            }
            
            _StopAnimationRoutine();
            _onFinishedCallback = null;
        }

        public bool PlayAnimation(MonoBehaviour animationScript, Action onFinishedCallback)
        {
            if (animationScript is null || onFinishedCallback is null)
            {
                return false;
            }

            _onFinishedCallback = onFinishedCallback;
            _StopAnimationRoutine();
            
            animationScript.enabled = true;
            animationScript.enabled = false;
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
        
        private UnityEvent _onAnimationFinished;

        private CoroutineItem _animationRoutine;
        private UnityAction _onAnimationFinishedHandler;
        private Action _onFinishedCallback;
    }
}