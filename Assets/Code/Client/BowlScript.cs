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
            
            AddListener(script.hoverEntered, args =>
            {
                script.GetComponent<MeshRenderer>().material = targets[1] as Material;
            });
            
            AddListener(script.hoverExited, args =>
            {
                script.GetComponent<MeshRenderer>().material = targets[2] as Material;
            });
        }

        public override void OnDestroy()
        {
            
        }
    }
}