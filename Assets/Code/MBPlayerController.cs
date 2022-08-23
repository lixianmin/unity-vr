
/********************************************************************
created:    2022-08-21
author:     lixianmin

参考: https://www.bilibili.com/video/BV1eK4y1R7fA?spm_id_from=333.337.search-card.all.click&vd_source=060cae0323076afc7bb35d1220dc6cf7

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using System.Collections.Generic;
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
        _moveAction = new InputAction("move", InputActionType.PassThrough);
        _moveAction.AddCompositeBinding("2DVector(mode=0)") // Or "Dpad"
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        
        _lookAction = new InputAction("look", InputActionType.PassThrough, "<Pointer>/delta");
        _rightMouseAction = new InputAction("rightMouse", InputActionType.PassThrough, "<Mouse>/rightButton");
    }
    
    private void OnEnable()
    {
        _EnableAction(_moveAction, ctx=>{
            _moveDirection = ctx.ReadValue<Vector2>();
        });
        
        _EnableAction(_lookAction, ctx =>
        {
            _lookRotate = ctx.ReadValue<Vector2>();
        });

        _EnableAction(_rightMouseAction, ctx =>
        {
            var click = ctx.ReadValue<float>();
            _isRightButtonDown = click > 0;
            Console.WriteLine(_isRightButtonDown);
        });        
    }
    
    private void OnDisable()
    {
        var count = _actionDisableHandlers.Count;
        if (count > 0)
        {
            for (var i = 0; i < count; i++)
            {
                var disableHandler = _actionDisableHandlers[i];
                disableHandler();
            }
            _actionDisableHandlers.Clear();
        }
    }

    private void Update()
    {
        _Look();
        _Move();
    }

    private void _Move()
    {
        if (_moveDirection.sqrMagnitude < 0.01)
        {
            return;
        }

        var motion = new Vector3(_moveDirection.x, 0, _moveDirection.y) * (MoveSpeed * Time.deltaTime);
        // _controller.Move(motion);
        _transform.Translate(motion);
    }

    private void _Look()
    {
        if (!_isRightButtonDown)
        {
            return;
        }
        
        var scaledRotateSpeed = RotateSpeed * Time.deltaTime;
        _transform.localRotation *= Quaternion.Euler(0, _lookRotate.x * scaledRotateSpeed, 0);
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
    
    private CharacterController _controller;
    private Transform _transform;
    private readonly List<Action> _actionDisableHandlers = new ();

    private InputAction _moveAction;
    private Vector2 _moveDirection;

    private InputAction _lookAction;
    private Vector2 _lookRotate;
    
    private InputAction _rightMouseAction;
    private bool _isRightButtonDown;
    private Vector3 _lastMousePosition;
}