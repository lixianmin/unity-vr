/********************************************************************
created:    2022-08-14
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unicorn.UI
{
    public class UITextMeshProUGUI : TextMeshProUGUI
    {
        // internal void AssignDefaultFont ()
        // {
        //     font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        // }

        public string GetGUID ()
        {
            return _guid;
        }

        [SerializeField] private string _guid = string.Empty;
    }
}