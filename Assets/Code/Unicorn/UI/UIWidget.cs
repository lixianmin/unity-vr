/********************************************************************
created:    2022-08-17
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/


using System;
using UnityEngine;

namespace Unicorn.UI
{
    public class UIWidget<T> where T : Component
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

        private T _widget;
        private string _name;
    }
}