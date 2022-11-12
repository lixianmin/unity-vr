/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn.UI;

namespace Client.UI
{
    public class UITop: UIWindowBase
    {
        public override string GetAssetPath()
        {
            return "Assets/res/prefabs/ui/uitop.prefab";
        }
        
        protected override void OnLoaded()
        {
            Console.WriteLine("top is OnLoaded");
        }
        
        protected override void OnOpened()
        {
            Console.WriteLine("top is OnOpened");
        }

        protected override void OnActivated()
        {
            Console.WriteLine("top is OnActivated");
        }

        protected override void OnDeactivating()
        {
            Console.WriteLine("top is OnDeactivating");
        }

        protected override void OnClosing()
        {
            Console.WriteLine("top is OnClosing");
        }

        protected override void OnUnloading()
        {
            Console.WriteLine("top is OnUnloading");
        }
        
        private readonly UIWidget<UIText> _title = new( "top_title");
        private readonly UIWidget<UIButton> _btnShop = new( "btn_top");
    }
}