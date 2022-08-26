
/********************************************************************
created:    2022-08-26
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using System.Collections;
using UnityEngine;

namespace Unicorn
{
    public class MBScriptProvider : MonoBehaviour
    {
        static MBScriptProvider()
        {
            foreach (var assembly in TypeTools.GetCustomAssemblies())
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                    if (type.IsSubclassOf(typeof(ScriptBase)))
                    {
                        var key = type.FullName ?? string.Empty;
                        _typeTable.Add(key, type);
                    }
                }
            } 
        }
        
        private void Awake()
        {
            var key = (fullScriptName ?? string.Empty).Trim();
            if (_typeTable[key] is Type type && Activator.CreateInstance(type) is ScriptBase script)
            {
                script._SetTargets(targets);
                CallbackTools.Handle(script.Awake, "[Awake()]");
                _script = script;
            }
        }
        
        private void OnDestroy()
        {
            var script = _script;
            if (script is not null)
            {
                CallbackTools.Handle(script.OnDestroy, "[OnDestroy()]");
                script.RemoveAllListeners();
            }
        }
        
        public string fullScriptName;
        public UnityEngine.Object[] targets;

        private ScriptBase _script;
        private static readonly Hashtable _typeTable = new (128);
    }
}