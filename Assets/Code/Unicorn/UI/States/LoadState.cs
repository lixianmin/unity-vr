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

        public void OnOpenWindow(UIWindowFetus fetus)
        {
            fetus.isDelayedCloseWindow = false;
        }

        public void OnCloseWindow(UIWindowFetus fetus)
        {
            fetus.isDelayedCloseWindow = true;
        }

        private void _Load(UIWindowFetus fetus)
        {
            var resourcePath = fetus.master.ResourcePath;
            if (string.IsNullOrEmpty(resourcePath))
            {
                Console.Error.WriteLine("resourcePath is emptye");
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
                    master.Release();
                } else if (isLoading)
                {
                    var goCloned = Object.Instantiate(prefab.MainAsset);
                    if (goCloned is not null)
                    {
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