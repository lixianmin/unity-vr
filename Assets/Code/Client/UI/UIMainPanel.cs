using System;
using System.Collections.Generic;
using Unicorn.UI;
using Unicorn.Web;
using UnityEngine;
using UnityEngine.Events;

namespace Client.UI
{
    public static class ExtendUnityEvent 
    {
        public static void AddListenerEx(this UnityEvent evt, UnityAction handler)
        {
            if (evt != null && handler != null)
            {
                evt.AddListener(handler);
            }
        }
    }

    public class UIMainPanel : UIPanel
    {
        protected override void Awake()
        {
            listener.AddListener(btnLoadCube.onClick, OnClickBtnLoadCube);
            listener.AddListener(btnLoadSphere.onClick, OnClickBtnLoadBag);
            listener.AddListener(btnGarbageCollect.onClick, OnClickBtnCollectGarbage);
            base.Awake();
        }

        protected override void OnDestroy()
        {
            listener.RemoveAllListeners();
            base.OnDestroy();
        }

        private void OnClickBtnLoadCube()
        {
            WebManager.LoadWebPrefab("Assets/res/prefabs/cube.prefab", prefab =>
            {
                var mainAsset = prefab.MainAsset;
                Console.WriteLine("load cube done, mainAsset={0}", mainAsset);
                GameObject.Instantiate(mainAsset);
            });
        }

        private void OnClickBtnLoadBag()
        {
            UIManager.OpenWindow(typeof(UIBag));
        }

        private void OnClickBtnCollectGarbage()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
            Console.WriteLine("gc done");
        }

        public UIInputField input;
        public UIButton btnLoadCube;
        public UIButton btnLoadSphere;
        public UIButton btnGarbageCollect;

        private readonly UIEventListener listener = new();
    }
}