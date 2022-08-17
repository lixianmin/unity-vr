/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using Unicorn.Collections;
using Unicorn.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class UIShop: UIWindowBase
    {
        public override string GetResourcePath()
        {
            return "Assets/res/prefabs/uishop.prefab";
        }
        
        private readonly UIWidget<UIText> _title = new( "shop_title");
        private readonly UIWidget<UIButton> _btnShop = new( "btn_shop");
        

        public override void OnLoaded()
        {
            _btnShop.GetWidget(this).onClick.AddListener(() =>
            {
                _title.GetWidget(this).text = "this is shop title";
            });
            
            Console.WriteLine("shop is OnLoaded");
        }
        
        public override void OnOpened()
        {
            Console.WriteLine("shop is OnOpened");
        }

        public override void OnActivated()
        {
            Console.WriteLine("shop is OnActivated");
        }

        public override void OnDeactivating()
        {
            Console.WriteLine("shop is OnDeactivating");
        }

        public override void OnClosing()
        {
            Console.WriteLine("shop is OnClosing");
        }

        public override void OnUnloading()
        {
            Console.WriteLine("shop is OnUnloading");
        }
    }
}