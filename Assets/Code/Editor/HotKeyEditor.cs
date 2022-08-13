
/********************************************************************
created:    2022-08-13
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/
using UnityEngine;
using UnityEditor;


namespace Unicorn.Menus
{
    public class HotKeyEditor : EditorWindow
    {
        [MenuItem("*HotKeys/Addressables", false, 0)]
        public static void OpenAddressables()
        {
            EditorApplication.ExecuteMenuItem("Window/Asset Management/Addressables/Groups");
        }
    }
}