﻿
/********************************************************************
created:    2022-08-12
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Unicorn.Web
{
    using UObject = UnityEngine.Object;

    public class WebItem : Disposable, IWebNode
    {
        internal WebItem(WebArgument argument, Action<WebItem> handler)
        {
            CoroutineManager.StartCoroutine(_CoLoad(argument, handler));
        }

        private IEnumerator _CoLoad(WebArgument argument, Action<WebItem> handler)
        {
            var fullpath = PathManager.GetFullPath(argument.localPath);
            var loadHandle = Addressables.LoadAssetAsync<UObject>(fullpath);
            _loadHandle = loadHandle;

            while (!loadHandle.IsDone)
            {
                yield return null;
            }

            IsDone = true;

            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                IsSucceeded = true;
                handler?.Invoke(this);
            }
        }

        protected override void _DoDispose(bool isManualDisposing)
        {
            Addressables.Release(_loadHandle);
        }

        public bool IsDone      { get; private set; }
        public bool IsSucceeded { get; private set; }
        public AsyncOperationHandle Handle { get { return _loadHandle; } }

        private AsyncOperationHandle _loadHandle;
    }
}