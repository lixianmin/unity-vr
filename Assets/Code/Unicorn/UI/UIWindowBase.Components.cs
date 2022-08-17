/********************************************************************
created:    2022-08-17
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unicorn.UI
{
    public partial class  UIWindowBase
    {
        private struct ComponentKey :IEquatable<ComponentKey>
        {
            public string name;
            public Type type;

            public override int GetHashCode()
            {
                return name.GetHashCode();
            }

            public bool Equals(ComponentKey right)
            {
                return name == right.name && type == right.type;
            }
            
            public override bool Equals(object right)
            {
                return null != right && Equals((ComponentKey)right);
            }
            
            public static bool operator == (ComponentKey left, ComponentKey right)
            {
                return left.Equals(right);
            }

            public static bool operator != (ComponentKey left, ComponentKey right)
            {
                return !(left == right);
            }
        }
        
        public Component GetComponent(string name, Type type)
        {
            if (string.IsNullOrEmpty(name) || type == null)
            {
                return null;
            }
            
            var key = new ComponentKey { name = name, type = type };
            if (_components.TryGetValue(key, out var widget))
            {
                return widget;
            }
            
            widget = _transform.DeepFindComponentEx(name, type);
            _components.Add(key, widget);
            return widget;
        }

        internal void _FillComponents(UISerializer serializer)
        {
            var dataList = serializer.widgetDatas;
            if (dataList is not null)
            {
                foreach (var data in dataList)
                {
                    var key = new ComponentKey { name = data.name, type = data.target.GetType() };
                    _components.Add(key, data.target);
                }
            }
        }

        private readonly Dictionary<ComponentKey, Component> _components = new(8);
    }
}