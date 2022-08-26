
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
            if (Activator.CreateInstance(typeof(BowlScript)) is ScriptBase script)
            {
                script._SetTargets(targets);
                CallbackTools.Handle(script.Awake, "[Awake()]");
            }
        }
        
        private void OnDestroy()
        {
            if (_script is not null)
            {
                CallbackTools.Handle(_script.OnDestroy, "[OnDestroy()]");    
            }
        }

        private ScriptBase _script;

        public string scriptName;
        public UnityEngine.Object[] targets;
    }
}