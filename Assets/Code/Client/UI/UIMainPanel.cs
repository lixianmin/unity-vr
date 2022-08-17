/********************************************************************
created:    2022-08-17
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn;
using Unicorn.UI;
using UnityEngine;

namespace Client.UI
{
    public class UIMainPanel : MonoBehaviour
    {
        protected void Awake()
        {
            listener.AddListener(btnOpenShop.onClick, OnClickOpenShop);
            listener.AddListener(btnOpenBag.onClick, OnClickOpenBag);
            listener.AddListener(btnGarbageCollect.onClick, OnClickBtnCollectGarbage);
        }

        protected void OnDestroy()
        {
            listener.RemoveAllListeners();
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