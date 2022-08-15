using System;
using TMPro;
using Unicorn.UI;
using Unicorn.Web;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Client.UI
{
    public class UIMainPanel : MonoBehaviour
    {
        private void Awake()
        {
            btnLoadCube.onClick.AddListener(onClickBtnLoadCube);
            btnLoadSphere.onClick.AddListener(onClickBtnLoadSphere);
            btnGarbageCollect.onClick.AddListener(onClickBtnCollectGarbage);
        }

        private void onClickBtnLoadCube()
        {
            WebManager.LoadWebPrefab("Assets/res/prefabs/cube.prefab", prefab =>
            {
                var mainAsset = prefab.MainAsset;
                Console.WriteLine("load cube done, mainAsset={0}", mainAsset);
                GameObject.Instantiate(mainAsset);
            });
        }

        private void onClickBtnLoadSphere()
        {
            WebManager.LoadWebPrefab("Assets/res/prefabs/sphere.prefab", prefab =>
            {
                GameObject.Instantiate(prefab.MainAsset);
                Console.WriteLine("load sphere done");
            });
        }

        private void onClickBtnCollectGarbage()
        {
            Resources.UnloadUnusedAssets();
            			GC.Collect();
            			Console.WriteLine("gc done");
        }

        public UIInputField input;
        public UIButton btnLoadCube;
        public UIButton btnLoadSphere;
        public UIButton btnGarbageCollect;
    }
}