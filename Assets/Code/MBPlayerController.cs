
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
        _inputActions = new();
    }
    
    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.gameplay.move.performed += _OnPlayerMovePerformed;
        _inputActions.gameplay.look.performed += _OnPlayerLookPerformed;
    }
    
    private void _OnPlayerMovePerformed(InputAction.CallbackContext ctx)
    {
       _moveDirection = ctx.ReadValue<Vector2>();
    }
    
    private void _OnPlayerLookPerformed(InputAction.CallbackContext ctx)
    {
        _lookDirection = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        _inputActions.gameplay.move.performed -= _OnPlayerMovePerformed;
        _inputActions.gameplay.look.performed -= _OnPlayerLookPerformed;
        _inputActions.Disable();
    }

    private void Update()
    {
        _Move(_moveDirection);
        _Look(_lookDirection);
    }
    
    private void _Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
        {
            return;
        }

        var scaledMoveSpeed = MoveSpeed * Time.deltaTime;
        // For simplicity's sake, we just keep movement in a single plane here. Rotate
        // direction according to world Y rotation of player.
        var motion = Quaternion.Euler(0, _transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        _transform.position += motion * scaledMoveSpeed;
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

    public float MoveSpeed = 10f;
    public float RotateSpeed = 1f;
    
    private Transform _transform;
    private CharacterController _controller;
    private PlayerInputActions _inputActions;
    
    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
}
