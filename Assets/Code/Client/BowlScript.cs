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
    public class BowlScript: ScriptBase
    {
        public override void Awake()
        {
            var targets = GetTargets();
            var script = targets[0] as XRGrabInteractable;
            script.hoverEntered.AddListener((args) =>
            {
                script.GetComponent<MeshRenderer>().material = targets[1] as Material;
            });
            
            script.hoverExited.AddListener((args) =>
            {
                script.GetComponent<MeshRenderer>().material = targets[2] as Material;
            });
        }
    }
}