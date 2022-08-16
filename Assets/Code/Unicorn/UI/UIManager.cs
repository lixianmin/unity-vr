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