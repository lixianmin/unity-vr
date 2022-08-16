/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn.Web;
using UnityEngine;

namespace Unicorn.UI.States
{
    internal class LoadState : StateBase
    {
        public override void OnEnter(UIWindowFetus fetus, object arg1)
        {
            AssertTools.IsTrue(!fetus.isDelayedCloseWindow);
            _loadWindowMask.OpenWindow();
            _Load(fetus);
        }

        public override void OnExit(UIWindowFetus fetus, object arg1)
        {
            _loadWindowMask.CloseWindow();
            AssertTools.IsTrue(!fetus.isDelayedCloseWindow);
        }

        public override void OnOpenWindow(UIWindowFetus fetus)
        {
            fetus.isDelayedCloseWindow = false;
        }

        public override void OnCloseWindow(UIWindowFetus fetus)
        {
            fetus.isDelayedCloseWindow = true;
        }

        private void _Load(UIWindowFetus fetus)
        {
            var resourcePath = fetus.master.GetResourcePath();
            if (string.IsNullOrEmpty(resourcePath))
            {
                Console.Error.WriteLine("resourcePath is empty.");
                return;
            }

            WebManager.LoadWebPrefab(resourcePath, prefab =>
            {
                var master = fetus.master;
                var isLoading = this == fetus.state;
                if (fetus.isDelayedCloseWindow)
                {
                    fetus.isDelayedCloseWindow = false;
                    fetus.ChangeState(StateKind.None);
                    master.Dispose();
                } 
                else if (isLoading)
                {
                    var mainAsset = prefab.MainAsset;
                    var goCloned = Object.Instantiate(mainAsset);
                    if (goCloned is not null)
                    {
                        goCloned.name = mainAsset.name;
                        fetus.OnLoadGameObject(goCloned);
                        master.OnLoaded();
                        fetus.isLoaded = true;
                        fetus.ChangeState(StateKind.OpenAnimation);
                    }
                    else
                    {
                        fetus.ChangeState(StateKind.Failure, prefab.ToString());
                    }
                }
                else
                {
                    Console.Error.WriteLine("invalid state={0}", fetus.state);
                }
                
                prefab.Dispose();
            });
        }
    }
}