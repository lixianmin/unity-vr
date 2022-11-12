
/********************************************************************
created:    2022-08-13
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using UnityEditor;

namespace Unicorn.Menus
{
    public class HotKeyEditor
    {
        [MenuItem("*HotKeys/Addressables", false, 0)]
        public static void OpenAddressables()
        {
            EditorApplication.ExecuteMenuItem("Window/Asset Management/Addressables/Groups");
        }
    }
}