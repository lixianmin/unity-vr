/********************************************************************
created:    2022-08-15
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System.Collections.Generic;
using UnityEngine.Events;

namespace Unicorn.UI
{
    public class UIEventListener : Disposable
    {
        private struct Item
        {
            public UnityEvent evt;
            public UnityAction handler;
        }
        
        public void AddListener(UnityEvent evt, UnityAction handler)
        {
            if (evt != null && handler != null)
            {
                evt.AddListener(handler);
                items.Add(new Item{evt = evt, handler = handler});
            }
        }

        public void RemoveAllListeners()
        {
            var count = items.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var item = items[i];
                    item.evt.RemoveListener(item.handler);
                }
                
                items.Clear();
            }
        }

        protected override void _DoDispose(bool isManualDisposing)
        {
            RemoveAllListeners();
        }

        private readonly List<Item> items = new(4);
    }
}