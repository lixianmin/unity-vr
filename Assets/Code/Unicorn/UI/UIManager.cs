/********************************************************************
created:    2022-08-15
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using System.Collections.Generic;
using Unicorn.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unicorn.UI
{
    public static class UIManager
    {
        private struct WindowItem
        {
            public int order;
            public UIWindowBase window;
        }
        
        public static void OpenWindow<T>()  where T : UIWindowBase
        {
            var t = typeof(T);
            var window = Activator.CreateInstance(typeof(T));
        }

        public static void LogicTick(float deltaTime)
        {
            
        }
        
        public static void Update(float deltaTime)
        {
        
        }

        private static UIWindowBase FetchWindow(Type windowType)
        {
            var item = IndexWindow(windowType);
            if (item.window == null)
            {
                if (Activator.CreateInstance(windowType) is not UIWindowBase window)
                {
                    throw new NullReferenceException("invalid windowType");
                }

                _windowsZOrder.Add(window);
                _version++;
                
                return window;
            }

            return item.window;
        }

        private static WindowItem IndexWindow(Type windowType)
        {
            var count = _windowsZOrder.Count;
            for (var order = count - 1; order >= 0; order--)
            {
                var window = _windowsZOrder[order];
                if (window.GetType() == windowType)
                {
                    return new WindowItem{order = order, window = window};
                }
            }

            return new WindowItem{order = -1, window = null};
        }

        internal static void _OnClosingWindow(UIWindowBase closingWindow)
        {
            var isClosingForeground = GetForegroundWindow() == closingWindow;
            if (isClosingForeground)
            {
                var nextForeground = _SearchNextForeground(closingWindow);
                _SetForegroundWindow(nextForeground);
            }
            else
            {
                _SendOnDeactivating(closingWindow);
            }
        }

        private static UIWindowBase _SearchNextForeground(UIWindowBase foreground)
        {
            var count = _windowsZOrder.Count;
            var found = false;
            for (var i = count - 1; i >= 0; i--)
            {
                var window = _windowsZOrder[i];
                if (window != foreground)
                {
                    // only window behind the closingWindow can be activated.
                    // only a isOpened window can be activated.
                    if (!window.GetFetus().isOpened) continue;
                    return found ? window : null;
                }

                found = true;
            }

            return null;
        }
        
        public static void SetForegroundWindow(UIWindowBase window)
        {
            var fetus = window?.GetFetus();
            if (fetus is not { isOpened: true })
            {
                return;
            }
            
            _SetForegroundWindow(window);
        }
        internal static void _SetForegroundWindow(UIWindowBase window)
        {
            var lastWindow = GetForegroundWindow();
            if (lastWindow != window)
            {
                _SendOnDeactivating(lastWindow);
                _foregroundWindow = window;
                _SendActivated(window);
            }
        }

        public static UIWindowBase GetForegroundWindow()
        {
            return _foregroundWindow;
        }

        private static void _SendOnDeactivating(UIWindowBase targetWindow)
        {
            targetWindow?.OnDeactivating();
        }

        private static void _SendActivated(UIWindowBase targetWindow)
        {
            if (targetWindow == null)
            {
                return;
            }

            var fetus = targetWindow.GetFetus();
            fetus.activateTime = Time.realtimeSinceStartup;

            _windowsZOrder.Sort((a, b) =>
            {
                var delta = a.GetRenderQueue() - b.GetRenderQueue();
                if (delta != 0)
                {
                    return delta;
                }

                var delta2 =  a.GetFetus().activateTime - b.GetFetus().activateTime;
                return (int) delta2;
            });

            _version++;
            _SortWindowsTransform();

            targetWindow.OnActivated();
        }

        private static void _SortWindowsTransform()
        {
            var count = _windowsZOrder.Count;
            if (count <= 1)
            {
                return;
            }

            var deltaIndex = 1;
            for (var i = 0; i < count; i++)
            {
                var window = _windowsZOrder[i];
                var transform = window.GetTransform();
                if (transform is not null)
                {
                    var targetIndex = i - deltaIndex;
                    var lastIndex = transform.GetSiblingIndex();
                    if (lastIndex != targetIndex)
                    {
                        transform.SetSiblingIndex(targetIndex);
                    }
                }
                else
                {
                    deltaIndex += 1;
                }
            }
        }
        public static Transform GetUIRoot()
        {
            if (null != _uiRoot) return _uiRoot;
            
            var goRoot = GameObject.Find("UIRoot");
            if (goRoot is null)
            {
                throw new NullReferenceException("can not find UIRoot");
            }
                
            Object.DontDestroyOnLoad(goRoot);
            _uiRoot = goRoot.transform;

            return _uiRoot;
        }

        private static readonly List<UIWindowBase> _windowsZOrder = new(4);
        private static int _version;
        
        private static UIWindowBase _foregroundWindow;
        private static Transform _uiRoot;
    }
}