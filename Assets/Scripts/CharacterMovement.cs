using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float moveSpeed = 3f;
    public float velocitySmoothTime = 0.05f;

    public Transform orientation;

    private Vector3 moveDirection;

    private float currentVelocityXRef;
    private float currentVelocityX;

    private float currentVelocityYRef;
    private float currentVelocityY;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RotateCharacter();
        CalculateVelocity();
    }

    public void RotateCharacter()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
        orientation.rotation = Quaternion.Slerp(orientation.rotation, lookRotation, 0.1f);
    }

    public void CalculateVelocity()
    {
        Vector3 targetVelocity = moveDirection * moveSpeed;

        currentVelocityX = Mathf.SmoothDamp(currentVelocityX, targetVelocity.x, ref currentVelocityXRef, velocitySmoothTime);
        currentVelocityY = Mathf.SmoothDamp(currentVelocityY, targetVelocity.y, ref currentVelocityYRef, velocitySmoothTime);

        rb.velocity = new Vector2(currentVelocityX, currentVelocityY);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().normalized;
    }
}
