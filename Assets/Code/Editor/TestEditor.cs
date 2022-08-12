
/********************************************************************
created:    2022-08-12
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Metadata.Menus
{
    public class TestEditor : EditorWindow
    {
        [MenuItem(EditorMetaTools.MenuRoot + "Test Editor", false, 109)]
        public static void OpenEditor()
        {
            var editor = EditorWindow.GetWindow<TestEditor>(false, "Test Editor", true);
            editor.Show();
        }

        private void OnFocus()
        {
           
        }
    }
}