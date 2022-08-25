/********************************************************************
created:    2022-08-17
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using Unicorn.UI;
using UnityEngine;

namespace Client.UI
{
    public class UIMain : UIWindowBase
    {
        public override string GetResourcePath()
        {
            return "Assets/res/prefabs/uimain.prefab";
        }
        
        private readonly UIWidget<UIButton> _btnOpenBag = new( "btn_open_bag");
        private readonly UIWidget<UIButton> _btnOpenShop = new( "btn_open_shop");
        private readonly UIWidget<UIButton> _btnOpenTop = new( "btn_open_top");
        private readonly UIWidget<UIButton> _btnCollectGarbage = new( "btn_collect_garbage");

        public override void OnLoaded()
        {
            _btnOpenBag.UI.onClick.AddListener(OnClickOpenBag);
            _btnOpenShop.UI.onClick.AddListener(OnClickOpenShop);
            _btnOpenTop.UI.onClick.AddListener(OnClickOpenTop);
            _btnCollectGarbage.UI.onClick.AddListener(OnClickBtnCollectGarbage);
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
    }
}