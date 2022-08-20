
/********************************************************************
created:    2022-08-21
author:     lixianmin

参考: https://www.bilibili.com/video/BV1eK4y1R7fA?spm_id_from=333.337.search-card.all.click&vd_source=060cae0323076afc7bb35d1220dc6cf7

Copyright (C) - All Rights Reserved
*********************************************************************/
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MBPlayerController : MonoBehaviour
{
    private void Start()
    {
        _transform = transform;
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _MoveLikeWow();
    }

    private void _MoveLikeWow()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var deltaTime = Time.deltaTime;

        // move
        var motion = _transform.forward * (MoveSpeed * vertical * deltaTime);
        _controller.Move(motion);
        
        // rotate
        _transform.Rotate(Vector3.up, horizontal*RotateSpeed);
    }

    public float MoveSpeed = 10f;
    public float RotateSpeed = 1f;
    
    private Transform _transform;
    private CharacterController _controller;
}
