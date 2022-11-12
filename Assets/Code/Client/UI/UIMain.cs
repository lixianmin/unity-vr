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
        public override string GetAssetPath()
        {
            return "uimain";
        }

        protected override void OnLoaded()
        {
            _btnOpenBag.UI.onClick.AddListener(OnClickOpenBag);
            _btnOpenTop.UI.onClick.AddListener(OnClickOpenTop);
            _btnCollectGarbage.UI.onClick.AddListener(OnClickBtnCollectGarbage);
            Console.WriteLine("uimain loaded");
        }
        
        private void OnClickOpenBag()
        {
            UIManager.Instance.OpenWindow(typeof(UIBag));
        }
        
        private void OnClickOpenTop()
        {
            UIManager.Instance.OpenWindow(typeof(UITop));
        }

        private void OnClickBtnCollectGarbage()
        {
            UIManager.Instance.CloseWindow(typeof(UIBag));
            UIManager.Instance.CloseWindow(typeof(UITop));
            
            Resources.UnloadUnusedAssets();
            GC.Collect();
            Console.WriteLine("gc done");
        }
        
        private readonly UIWidget<UIButton> _btnOpenBag = new( "btn_open_bag");
        private readonly UIWidget<UIButton> _btnOpenTop = new( "btn_open_top");
        private readonly UIWidget<UIButton> _btnCollectGarbage = new( "btn_collect_garbage");
    }
}