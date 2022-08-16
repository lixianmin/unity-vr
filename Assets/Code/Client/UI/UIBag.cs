/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn.UI;

namespace Client.UI
{
    public class UIBag: UIWindowBase
    {
        public override void OnActivated()
        {
            Console.WriteLine("bag is activated");
        }

        public override string GetResourcePath()
        {
            return "Assets/res/prefabs/uibag.prefab";
        }
    }
}