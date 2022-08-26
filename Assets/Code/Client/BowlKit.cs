/********************************************************************
created:    2022-08-26
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unicorn;

namespace Client
{
    public class BowlKit: KitBase
    {
        public override void Awake()
        {
            var assets = GetAssets();
            if (assets.Length < 3)
            {
                return;
            }
            
            var script = assets[0] as XRGrabInteractable;
            AddListener(script.hoverEntered, args =>
            {
                script.GetComponent<MeshRenderer>().material = assets[1] as Material;
            });
            
            AddListener(script.hoverExited, args =>
            {
                script.GetComponent<MeshRenderer>().material = assets[2] as Material;
            });
        }

        public override void OnDestroy()
        {
            Console.WriteLine("@@@ OnDestroy()");
        }
    }
}