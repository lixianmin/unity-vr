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

        public string GetName()
        {
            return _name;
        }
        
        protected UIWindowBase _window;
        protected string _name;
    }
    
    public class UIWidget<T> : UIWidgetBase where T : Component
    {
        public UIWidget(string name)
        {
            _name = name;
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
    }
}