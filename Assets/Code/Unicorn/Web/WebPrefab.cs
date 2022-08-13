
/********************************************************************
created:    2022-08-13
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using UnityEngine;
using Unicorn.Web.Internal;


namespace Unicorn.Web
{
    public partial class WebPrefab: Disposable, IWebNode
    {
        internal WebPrefab(WebArgument argument, Action<WebPrefab> handler)
        {
            _webItem = new WebItem(argument, webItem =>
            {
                if (!webItem.IsSucceeded) return;
                if (webItem.Asset is not GameObject mainAsset) return;

                var script = mainAsset.GetComponent<MBPrefabAid>() ?? mainAsset.AddComponent<MBPrefabAid>();
                script.localPath = argument.localPath;
                PrefabRecycler.AddPrefab(argument.localPath, this);
                
                HandleTools.SafeHandle(handler, this);
            });
        }

        protected override void _DoDispose(bool isManualDisposing)
        {
            PrefabRecycler.RemoveReference(_webItem.LocalPath);
        }

        public bool IsDone => _webItem.IsDone;
        public bool IsSucceeded => _webItem.IsSucceeded;
        public GameObject MainAsset => _webItem.Asset as GameObject;

        private WebItem _webItem;
    }
}