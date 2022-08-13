
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
            _argument = argument;
            CoroutineManager.StartCoroutine(_CoLoad(argument, handler));
        }

        private IEnumerator _CoLoad(WebArgument argument, Action<WebItem> handler)
        {
            var fullPath = PathManager.GetFullPath(argument.localPath);
            var loadHandle = Addressables.LoadAssetAsync<UObject>(fullPath);
            _loadHandle = loadHandle;

            while (!loadHandle.IsDone)
            {
                yield return null;
            }

            IsDone = true;

            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                IsSucceeded = true;
                HandleTools.SafeHandle(handler, this);
            }
        }

        protected override void _DoDispose(bool isManualDisposing)
        {
            Addressables.Release(_loadHandle);
            Console.WriteLine("[_DoDispose()] localPath={0}", _argument.localPath);
        }

        public bool IsDone      { get; private set; }
        public bool IsSucceeded { get; private set; }
        public string LocalPath => _argument.localPath;

        public UObject Asset => _loadHandle.Result as UObject;

        private WebArgument _argument;
        private AsyncOperationHandle _loadHandle;
    }
}