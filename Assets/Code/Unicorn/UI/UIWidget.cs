/********************************************************************
created:    2022-08-17
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using UnityEngine;

namespace Unicorn.UI
{
    public abstract class UIWidgetBase
    {
        internal void _SetWindow(UIWindowBase window)
        {
            _window = window;
        }
        
        protected UIWindowBase _window;
    }
    
    public class UIWidget<T> : UIWidgetBase where T : Component
    {
        public UIWidget(string name)
        {
            _name = name;
        }

        public T GetWidget(UIWindowBase window)
        {
            if (_widget == null)
            {
                _widget = window.GetWidget(_name, typeof(T)) as T;
            }

            return _widget;
        }

        public T UI
        {
            get
            {
                if (_widget == null && _window != null)
                {
                    _widget = _window.GetWidget(_name, typeof(T)) as T;
                }

                return _widget;
            }
        }

        private T _widget;
        private string _name;
    }
}