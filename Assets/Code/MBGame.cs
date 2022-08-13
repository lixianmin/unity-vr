
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
using Unicorn.Web;

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
		UnicornMain.Instance.Tick(deltaTime);
		_game.Tick(deltaTime);
	}

    private void OnGUI()
    {
		const float step = 200;
        if (GUI.Button(new Rect(10, 10, 200, 100), "load cube"))
        {
	        WebManager.LoadWebPrefab("res/prefabs/cube.prefab", prefab =>
	        {
		        var mainAsset = prefab.MainAsset;
				Console.WriteLine("load cube done, mainAsset={0}", mainAsset);
				GameObject.Instantiate(mainAsset);
	        });
        }

		if (GUI.Button(new Rect(10, step, 100, 50), "load sphere"))
		{
			WebManager.LoadWebPrefab("res/prefabs/sphere.prefab", prefab =>
			{
				GameObject.Instantiate(prefab.MainAsset);
				Console.WriteLine("load sphere done");
			});
		}


		if (GUI.Button(new Rect(10, 2* step, 100, 50), "unload cube"))
		{

		}

		if (GUI.Button(new Rect(10, 3* step, 100, 50), "unload sphere"))
		{

		}

		if (GUI.Button(new Rect(10, 4 * step, 100, 50), "gc"))
		{
			Resources.UnloadUnusedAssets();
			GC.Collect();
			Console.WriteLine("gc done");
		}
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
		yield return null;
	}

	private readonly Game _game = Game.Instance;
}
