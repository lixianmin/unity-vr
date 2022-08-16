using System;
using System.Collections.Generic;
using Unicorn.UI;
using Unicorn.Web;
using UnityEngine;
using UnityEngine.Events;

namespace Client.UI
{
    public static class ExtendUnityEvent 
    {
        public static void AddListenerEx(this UnityEvent evt, UnityAction handler)
        {
            if (evt != null && handler != null)
            {
                evt.AddListener(handler);
            }
        }
    }

    public class UIMainPanel : UIPanel
    {
        protected override void Awake()
        {
            listener.AddListener(btnOpenShop.onClick, OnClickOpenShop);
            listener.AddListener(btnOpenBag.onClick, OnClickOpenBag);
            listener.AddListener(btnGarbageCollect.onClick, OnClickBtnCollectGarbage);
            base.Awake();
        }

        protected override void OnDestroy()
        {
            listener.RemoveAllListeners();
            base.OnDestroy();
        }

        private void OnClickOpenShop()
        {
            UIManager.OpenWindow(typeof(UIShop));
        }

        private void OnClickOpenBag()
        {
            UIManager.OpenWindow(typeof(UIBag));
        }

        private void OnClickBtnCollectGarbage()
        {
            UIManager.CloseWindow(typeof(UIShop));
            UIManager.CloseWindow(typeof(UIBag));
            
            // Resources.UnloadUnusedAssets();
            // GC.Collect();
            Console.WriteLine("gc done");
        }

        public UIInputField input;
        public UIButton btnOpenShop;
        public UIButton btnOpenBag;
        public UIButton btnGarbageCollect;

        private readonly UIEventListener listener = new();
    }
}