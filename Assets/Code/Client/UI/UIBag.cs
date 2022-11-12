/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using System.Collections.Generic;
using Unicorn;
using Unicorn.UI;

namespace Client.UI
{
    public class UIBag: UIWindowBase
    {
        public override string GetAssetPath()
        {
            return "Assets/res/prefabs/ui/uibag.prefab";
        }
        
        protected override void OnLoaded()
        { 
            UIWidget<UIText> title = new( "title", this);
            _btnBag.UI.onClick.AddListener(()=>{
                Console.WriteLine("click button");
                title.UI.text = "hello world";
            });
            
            Console.WriteLine("bag is OnLoaded");
        }
        
        protected override void OnOpened()
        {
            Console.WriteLine("bag is OnOpened");
        }

        protected override void OnActivated()
        {
            Console.WriteLine("bag is OnActivated");
        }

        protected override void OnDeactivating()
        {
            Console.WriteLine("bag is OnDeactivating");
        }

        protected override void OnClosing()
        {
            Console.WriteLine("bag is OnClosing");
        }

        protected override void OnUnloading()
        {
            Console.WriteLine("bag is OnUnloading");
        }
        
        private readonly UIWidget<UIButton> _btnBag = new(  "btn_bag");
    }
}