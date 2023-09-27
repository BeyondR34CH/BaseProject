using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedUp;
    [SerializeField] private float slowDown;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody rigid;

    [ShowInInspector, ReadOnly] 
    public float MoveSpeed { get; private set; }
    public Vector2 MoveInput { get; private set; }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        var vcam = CameraManager.MainCamera.GetComponentInChildren<CinemachineVirtualCamera>();
        if (vcam)
        {
            vcam.Follow = transform;
            vcam.LookAt = transform;
        }

        InputModule.Input.onActionTriggered += OnActionTriggered;
    }

    private void FixedUpdate()
    {
        var moveVelocity = rigid.velocity; moveVelocity.y = 0;
        var moveDirection = moveVelocity.normalized;
        var moveSpeed = moveVelocity.magnitude;

        MoveSpeed = moveSpeed;

        if (MoveInput != Vector2.zero)
        {
            var inputDirection = MoveInput.To3();
            var inputSpeed = Vector3.Dot(moveVelocity, inputDirection);
            if (inputSpeed < maxSpeed)
            {
                rigid.AddForce(inputDirection * speedUp);
            }
            else
            {
                VelocityDamp(moveDirection, moveSpeed, slowDown);
            }

            var sideDirection = Quaternion.Euler(0, 90, 0) * inputDirection;
            var sideSpeed = Vector3.Dot(moveVelocity, sideDirection);
            if (sideSpeed < 0)
            {
                sideDirection = -sideDirection;
                sideSpeed = -sideSpeed;
            }
            VelocityDamp(sideDirection, sideSpeed, slowDown);
        }
        else
        {
            VelocityDamp(moveDirection, moveSpeed, slowDown);
        }
    }

    private void OnDisable()
    {
        InputModule.Input.onActionTriggered -= OnActionTriggered;
    }

    private void VelocityDamp(Vector3 dampDir, float dampSpeed, float damp)
    {
        var slowSpeed = damp * Time.fixedDeltaTime / rigid.mass;
        if (dampSpeed > slowSpeed)
        {
            rigid.velocity -= slowSpeed * dampDir;
        }
        else if (dampSpeed > 0.2f)
        {
            rigid.velocity -= dampSpeed * dampDir;
        }
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":
                MoveInput = context.ReadValue<Vector2>();
                break;
            case "Jump":
                if (!context.performed) break;
                //rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                rigid.AddForce(transform.forward * jumpForce, ForceMode.Impulse);
                break;
            case "Fire":
                if (!context.performed) break;
                rigid.velocity = Vector3.zero;
                var ray = CameraManager.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out var hit, 100, Ref.AimLayer))
                {
                    var dir = hit.point - transform.position; dir.y = 0;
                    rigid.AddForce(dir.normalized * jumpForce, ForceMode.Impulse);
                }
                break;
        };
    }
}
