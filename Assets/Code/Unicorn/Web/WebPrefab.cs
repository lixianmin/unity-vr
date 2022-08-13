
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
            new WebItem(argument, webItem =>
            {
                if (!webItem.IsSucceeded) return;
                if (webItem.Asset is not GameObject mainAsset) return;

                var script = mainAsset.GetComponent<MBPrefabAid>() ?? mainAsset.AddComponent<MBPrefabAid>();
                script.localPath = argument.localPath;
                PrefabRecycler.TryAddPrefab(argument.localPath, this);

                // 这里的handler是有可能立即调用到的, 所以不能外面new WebItem()返回值的时候设置_webItem
                _webItem = webItem;
                HandleTools.SafeHandle(handler, this);
            });
        }

        protected override void _DoDispose(bool isManualDisposing)
        {
            // Console.WriteLine("[_DoDispose()] {0}", this.ToString());
        }

        public override string ToString()
        {
            return $"WebPrefab: id={_id.ToString()}, localPath={_webItem.LocalPath}";
        }

        public bool IsDone => _webItem.IsDone;
        public bool IsSucceeded => _webItem.IsSucceeded;
        public GameObject MainAsset => _webItem.Asset as GameObject;

        private WebItem _webItem;
        private readonly int _id = WebTools.GetNextId();
    }
}