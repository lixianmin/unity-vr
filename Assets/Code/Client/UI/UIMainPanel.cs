/********************************************************************
created:    2022-08-17
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using Unicorn;
using Unicorn.UI;
using UnityEngine;

namespace Client.UI
{
    public class UIMainPanel : MonoBehaviour
    {
        protected void Awake()
        {
            btnOpenBag.onClick.AddListener(OnClickOpenBag);
            btnOpenShop.onClick.AddListener(OnClickOpenShop);
            btnOpenTop.onClick.AddListener(OnClickOpenTop);

            btnGarbageCollect.onClick.AddListener(OnClickBtnCollectGarbage);
        }

        protected void OnDestroy()
        {
            
        }

        private void OnClickOpenBag()
        {
            UIManager.OpenWindow(typeof(UIBag));
        }
        
        private void OnClickOpenShop()
        {
            UIManager.OpenWindow(typeof(UIShop));
        }
        
        private void OnClickOpenTop()
        {
            UIManager.OpenWindow(typeof(UITop));
        }

        private void OnClickBtnCollectGarbage()
        {
            UIManager.CloseWindow(typeof(UIShop));
            UIManager.CloseWindow(typeof(UIBag));
            UIManager.CloseWindow(typeof(UITop));
            
            Resources.UnloadUnusedAssets();
            GC.Collect();
            Console.WriteLine("gc done");
        }

        public UIButton btnOpenBag;
        public UIButton btnOpenShop;
        public UIButton btnOpenTop;

        public UIButton btnGarbageCollect;
    }
}