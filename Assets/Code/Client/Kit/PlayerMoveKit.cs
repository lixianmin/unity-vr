
/********************************************************************
created:    2022-08-27
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Client
{
    public class PlayerMoveKit: KitBase
    {
        public override void Awake()
        {
            var transform = GetTransform();
            _leftController= transform.DeepFindComponentEx("LeftHand Controller", typeof(XRController)) as XRController;
        }

        public override void Update()
        {
            if (_leftController is null)
            {
                return;
            }

            var transform = GetTransform();
            var isPressing = _leftController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out var delta);
            if (isPressing)
            {
                const float moveSpeed = 2.0f;
                delta *= moveSpeed * Time.deltaTime;
                
                var lastPos = transform.position;
                transform.position = new Vector3(lastPos.x + delta.x, lastPos.y, lastPos.z + delta.y);
            }
        }

        private XRController _leftController;
    }
}