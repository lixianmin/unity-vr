
/********************************************************************
created:    2022-08-26
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unicorn.Kit;

namespace Client
{
    public class BowlKit: KitBase
    {
        protected override void Awake()
        {
            var assets = GetAssets();
            if (assets.Length < 3)
            {
                return;
            }
            
            var script = assets[0] as XRGrabInteractable;
            _listener.AddListener(script!.hoverEntered, args =>
            {
                script.GetComponent<MeshRenderer>().material = assets[1] as Material;
            });
            
            _listener.AddListener(script.hoverExited, args =>
            {
                script.GetComponent<MeshRenderer>().material = assets[2] as Material;
            });
        }

        protected override void OnDestroy()
        {
            _listener.RemoveAllListeners();
            Console.WriteLine("OnDestroy()");
        }

        private readonly EventListener _listener = new();
    }
}