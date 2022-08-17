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
        public override string GetResourcePath()
        {
            return "Assets/res/prefabs/uitop.prefab";
        }
        
        private readonly UIWidget<UIText> _title = new( "top_title");
        private readonly UIWidget<UIButton> _btnShop = new( "btn_top");
        

        public override void OnLoaded()
        {
            Console.WriteLine("top is OnLoaded");
        }
        
        public override void OnOpened()
        {
            Console.WriteLine("top is OnOpened");
        }

        public override void OnActivated()
        {
            Console.WriteLine("top is OnActivated");
        }

        public override void OnDeactivating()
        {
            Console.WriteLine("top is OnDeactivating");
        }

        public override void OnClosing()
        {
            Console.WriteLine("top is OnClosing");
        }

        public override void OnUnloading()
        {
            Console.WriteLine("top is OnUnloading");
        }
    }
}