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

        public override void OnLoaded()
        {
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