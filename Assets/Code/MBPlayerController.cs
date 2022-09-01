
/********************************************************************
created:    2022-08-21
author:     lixianmin

参考: https://www.bilibili.com/video/BV1eK4y1R7fA?spm_id_from=333.337.search-card.all.click&vd_source=060cae0323076afc7bb35d1220dc6cf7

    https://blog.csdn.net/weixin_43147385/article/details/126566920

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using System.Collections.Generic;
using Unicorn;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MBPlayerController : MonoBehaviour
{
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _transform = transform;
        _initInputAction();
    }

    private void _initInputAction()
    {
        // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.3/manual/ActionBindings.html#2d-vector
        var moveAction = new InputAction("move", InputActionType.PassThrough);
        moveAction.AddCompositeBinding("2DVector(mode=0)") // Or "Dpad"
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // 针对Keyboard来讲:
        //  InputActionType.Value => keydown返回1, keyup无返回
        //  InputActionType.PassThrough => keydown读到1, keyup读到0
        var jumpAction = new InputAction("jump", InputActionType.Value, "<Keyboard>/space");        
        var lookAction = new InputAction("look", InputActionType.PassThrough, "<Pointer>/delta");
        var rightMouseAction = new InputAction("rightMouse", InputActionType.PassThrough, "<Mouse>/rightButton");
        
        _onEnableAction = () =>
        {
            _EnableAction(moveAction, ctx=>
            { 
                var direction = ctx.ReadValue<Vector2>();
                var motion = _transform.rotation *  new Vector3(direction.x, 0, direction.y) * MoveSpeed;
                _moveVeloctity.x = motion.x;
                _moveVeloctity.z = motion.z;
            });

            _EnableAction(jumpAction, ctx =>
            {
                _moveVeloctity.y = math.sqrt(2 * Gravity * JumpHeight);
                // Console.WriteLine(ctx.ReadValue<float>());
            });
        
            _EnableAction(lookAction, ctx =>
            {
                _lookRotate = ctx.ReadValue<Vector2>();
            });

            _EnableAction(rightMouseAction, ctx =>
            {
                var click = ctx.ReadValue<float>();
                _isRightButtonDown = click > 0;
            });   
        };
    }
    
    private void OnEnable()
    {
        _onEnableAction();
    }
    
    private void OnDisable()
    {
        _actionDisableHandlers.InvokeAndClearEx();
    }

    private void Update()
    {
        _Look();
        _MoveHeight();
        
        // 因为controller自己有minMoveDistance的设置, 所以不需要使用deadZone进行限制
        _controller.Move(_moveVeloctity * Time.deltaTime);
    }

    private void _MoveHeight()
    {
        // todo 目前有一个isGrounded反复变化的问题, 不知道怎么解
        if (_controller.isGrounded)
        {
            // 如果角色接触地面，则向下的速度置0
            if (_moveVeloctity.y < 0)
            {
                _moveVeloctity.y = 0;
            }
        }
        else
        {   // 如果角色在空中, 则要基于重力给一个向下的速度
            _moveVeloctity.y -= Gravity * Time.deltaTime;
        }
        
        // Console.WriteLine($"_moveVeloctity={_moveVeloctity}, isGrounded={_controller.isGrounded}");
    }

    private void _Look()
    {
        if (!_isRightButtonDown)
        {
            return;
        }
        
        var scaledRotateSpeed = RotateSpeed * Time.deltaTime;
        _transform.localRotation *= quaternion.Euler(0, _lookRotate.x * scaledRotateSpeed, 0);
    }
    
    private void _EnableAction(InputAction action, Action<InputAction.CallbackContext> handler)
    {
        action.performed += handler;
        action.Enable();
        
        _actionDisableHandlers.Add(() =>
        {
            action.performed -= handler;
            action.Disable();
        });
    }

    public float MoveSpeed = 5f;
    public float RotateSpeed = 3f;
    
    public float Gravity = 9.8f;    // 重力加速度
    public float JumpHeight = 1.2f; // 跳跃的高度

    private CharacterController _controller;
    private Transform _transform;

    private Action _onEnableAction;
    private readonly List<Action> _actionDisableHandlers = new ();

    private Vector3 _moveVeloctity = Vector3.zero;
    private Vector2 _lookRotate = Vector2.zero;
    private bool _isRightButtonDown;
}