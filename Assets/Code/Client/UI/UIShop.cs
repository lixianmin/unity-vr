/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn.UI;

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
            _btnShop.UI.onClick.AddListener(() =>
            {
                _title.UI.text = "this is shop title";
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