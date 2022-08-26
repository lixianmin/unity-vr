
/********************************************************************
created:    2022-08-26
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using UnityEngine;

namespace Unicorn.Scripts
{
    public class MBScriptProvider : MonoBehaviour
    {
        private void Awake()
        {
            
            var script = Activator.CreateInstance(typeof(BowlScript));
        }

        public MonoBehaviour target;
        public string scriptName;
    }
}