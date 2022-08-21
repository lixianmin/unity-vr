
/********************************************************************
created:    2022-08-21
author:     lixianmin

参考: https://www.bilibili.com/video/BV1eK4y1R7fA?spm_id_from=333.337.search-card.all.click&vd_source=060cae0323076afc7bb35d1220dc6cf7

Copyright (C) - All Rights Reserved
*********************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MBPlayerController : MonoBehaviour
{
    private void Awake()
    {
        _transform = transform;
        _controller = GetComponent<CharacterController>();
        _initInputAction();
    }

    private void _initInputAction()
    {
        _moveInputAction = new InputAction("Horizontal", InputActionType.PassThrough);
        _moveInputAction.AddCompositeBinding("2DVector") // Or "Dpad"
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
    }
    
    private void OnEnable()
    {
        _moveInputAction.Enable();
        _moveInputAction.performed += _OnMovePerformed;
    }
    
    private void _OnMovePerformed(InputAction.CallbackContext ctx)
    {
        _moveDirection = ctx.ReadValue<Vector2>();
    }
    
    private void _OnPlayerLookPerformed(InputAction.CallbackContext ctx)
    {
        _lookDirection = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        _moveInputAction.performed += _OnMovePerformed;
        _moveInputAction.Disable();
    }

    private void Update()
    {
        // _Look(_lookDirection);
        _Move();
    }

    // private void _MoveLikeWow()
    // {
    //     // var horizontal = _horizontalInputAction.ReadValue<float>();
    //     // var vertical = _verticalInputAction.ReadValue<float>();
    //     // var deltaTime = Time.deltaTime;
    //     //
    //     // // move
    //     // var motion = _transform.forward * (MoveSpeed * vertical * deltaTime);
    //     // _controller.Move(motion);
    //     //
    //     // // rotate
    //     // _transform.Rotate(Vector3.up, horizontal * RotateSpeed);
    // }
    
    private void _Move()
    {
        var direction = _moveDirection;
        if (direction.sqrMagnitude < 0.01)
        {
            return;
        }

        var motion = new Vector3(direction.x, 0, direction.y) * (MoveSpeed * Time.deltaTime);
        _controller.Move(motion);
    }

    private void _Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.01)
        {
            return;
        }

        var scaledRotateSpeed = RotateSpeed * Time.deltaTime;
        _lookDirection.y += rotate.x * scaledRotateSpeed;
        _lookDirection.x = Mathf.Clamp(_lookDirection.x - rotate.y * scaledRotateSpeed, -89, 89);
        transform.localEulerAngles = _lookDirection;
    }

    public float MoveSpeed = 5f;
    public float RotateSpeed = .2f;
    
    private Transform _transform;
    private CharacterController _controller;
    private InputAction _moveInputAction;
    
    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
}
