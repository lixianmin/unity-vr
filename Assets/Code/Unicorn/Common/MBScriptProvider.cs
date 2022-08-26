
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
                        _typeTable.Add(type.FullName?? string.Empty, type);
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

        public string fullScriptName;
        public UnityEngine.Object[] targets;

        private static readonly Hashtable _typeTable = new (128);
    }
}