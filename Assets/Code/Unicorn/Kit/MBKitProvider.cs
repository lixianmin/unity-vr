
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
    public class MBKitProvider : MonoBehaviour
    {
        static MBKitProvider()
        {
            foreach (var assembly in TypeTools.GetCustomAssemblies())
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                    if (type.IsSubclassOf(typeof(KitBase)))
                    {
                        var key = type.FullName ?? string.Empty;
                        _typeTable.Add(key, type);
                    }
                }
            } 
        }
        
        private void Awake()
        {
            var key = (fullKitName ?? string.Empty).Trim();
            if (_typeTable[key] is Type type && Activator.CreateInstance(type) is KitBase kit)
            {
                _kit = kit;
                kit._SetAssets(assets);
                CallbackTools.Handle(kit.Awake, "[Awake()]");
            }
            else
            {
                Console.Error.WriteLine("invalid fullKitName={0}", fullKitName);
            }
        }
        
        private void OnDestroy()
        {
            var kit = _kit;
            if (kit is not null)
            {
                CallbackTools.Handle(kit.OnDestroy, "[OnDestroy()]");
                kit.RemoveAllListeners();
            }
        }

        private void Update()
        {
            var kit = _kit;
            if (kit is not null)
            {
                CallbackTools.Handle(kit.Update, "[Update()]");
            }
        }
        
        /// <summary>
        /// 包含namespace的kit脚本全称, 用于生成kit脚本
        /// </summary>
        public string fullKitName;
        
        /// <summary>
        /// 关联场景资源, 用于kit脚本逻辑
        /// </summary>
        public UnityEngine.Object[] assets;

        private KitBase _kit;
        private static readonly Hashtable _typeTable = new (128);
    }
}