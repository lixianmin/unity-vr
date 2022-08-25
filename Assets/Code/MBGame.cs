
/********************************************************************
created:    2022-08-12
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/
#pragma warning disable 0436

using System.IO;
using System.Collections;
using UnityEngine;
using Unicorn;
using Metadata;
using Client;
using System;
using Client.UI;
using Unicorn.UI;

public class MBGame : MonoBehaviour
{
	private void Start()
	{
		UnicornMain.Instance.Init();
		CoroutineManager.StartCoroutine(_CoLoadMetadata());

		//DependencyManager.Instance.Init();
		// AssetManager.Instance.RequestAsset("globals.ab", "", (assetReference)=>{
		// 	Debug.LogError("assetReference : " + assetReference);
		// 	Debug.LogError("assetReference.assetBundle : " + assetReference.assetBundle);
		// });
		//WebManager.SetBaseUrl(PathTools.DefaultBaseUrl);
		// AssetManager.Instance.RequestGameObject("prefabs/ui/_battle.ab", "uibattle_stage", (go)=>
		// {
		// 	Console.Error.WriteLine("go : " + go);
		// });

		// AssetManager.Instance.RequestAsset("prefabs/atlas/_chapter.ab", "chapter_checkbox_b05_background", (obj)=>{
		// 	assetReference = obj;
		// 	Console.Error.WriteLine("obj : " + obj + " " + assetReference.asset);
		// 	assetReference.Dispose();
		// });
		//WebManager.LoadWebPrefab("prefabs/ui/_battle.ab/uibattle_stage", (webItem) =>
		//{
		//	Console.Error.WriteLine("webItem : " + webItem + " " + webItem.mainAsset);
		//	webItem.CloneMainAsset();
		//	// GameObject.Instantiate(webItem.mainAsset);
		//});
	}

	private void Update()
	{
		var deltaTime = Time.deltaTime;
		UnicornMain.Instance.Update(deltaTime);
		_game.Update(deltaTime);
	}

	private IEnumerator _CoLoadMetadata()
    {
		var metadataManager = MetadataManager.Instance as GameMetadataManager;
		if (metadataManager == null)
		{
			yield break;
		}
		
		var fullpath = "/Users/xmli/code/unity-vr/resource/android/metadata.raw";
		if (!fullpath.IsNullOrEmptyEx())
        {
            try
            {
				var stream = File.OpenRead(fullpath);
				metadataManager.LoadRawStream(stream);
            }catch(Exception ex)
            {
				Console.Error.WriteLine("[_CoLoadMetadata()] load metadata failed, ex={0}", ex.ToString());
			}
		}


		var version = metadataManager.GetMetadataVersion();
		Console.WriteLine("[_CoLoadMetadata()] Metadata Loaded, metadataVersion={0}.", version.ToString());

		// UIManager.OpenWindow(typeof(UIMain));
		UIManager.OpenWindow(typeof(UIAdjustNumber));
    }

	private readonly Game _game = Game.Instance;
}
