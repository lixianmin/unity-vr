
/********************************************************************
created:    2022-08-27
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Client.UI;
using Unicorn;
using Unicorn.UI;
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
            _leftController = transform.DeepFindComponentEx("LeftHand Controller", typeof(XRController)) as XRController;
        }

        public override void Update()
        {
            if (_leftController is null)
            {
                return;
            }

            var transform = GetTransform();
            var isOk = _leftController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out var delta);

            const float squaredDeadZone = 0.001f;
            if (isOk && delta.sqrMagnitude > squaredDeadZone)
            {
                const float moveSpeed = 2.0f;
                var motion = transform.rotation * new Vector3(delta.x, 0, delta.y) * moveSpeed * Time.deltaTime;                
                transform.Translate(motion, Space.World);
            }

            // 打印一些debug的信息到ui里
            if (Time.time > _lastPrintTime)
            {
                _lastPrintTime = Time.time + 1;
                var text = $"transform={transform.rotation}";
                
                _debugUI ??= UIManager.GetWindow<UIAdjustNumber>();
                _debugUI?.SetDebugText(text);
                Console.WriteLine(text);
            }
        }

        private float _lastPrintTime;

        // 左手移动, 右手旋转
        private XRController _leftController;
        private UIAdjustNumber _debugUI;
    }
}