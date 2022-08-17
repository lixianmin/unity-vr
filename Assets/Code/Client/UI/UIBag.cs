/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using System.Collections.Generic;
using Unicorn.Collections;
using Unicorn.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class UIBag: UIWindowBase
    {
        public override void OnLoaded()
        {
            Console.WriteLine("bag is OnLoaded");
        }
        
        public override void OnOpened()
        {
            Console.WriteLine("bag is OnOpened");
        }

        public override void OnActivated()
        {
            Console.WriteLine("bag is OnActivated");
        }

        public override void OnDeactivating()
        {
            Console.WriteLine("bag is OnDeactivating");
        }

        public override void OnClosing()
        {
            Console.WriteLine("bag is OnClosing");
        }

        public override void OnUnloading()
        {
            Console.WriteLine("bag is OnUnloading");
        }

        public override string GetResourcePath()
        {
            return "Assets/res/prefabs/uibag.prefab";
        }

        public override UILayout[] GetLayouts()
        {
            return new UILayout[]
            {
                new() { name = "title", type = typeof(UIText) },
                new() { name = "btn_bag", type = typeof(UIButton) },
            };
        }
    }
}