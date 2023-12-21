using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float velocitySmoothTime = 0.05f;

    [Header("Orientation Settings")]
    public Transform orientation;
    public float rotationSpeed;

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
        if (moveDirection.magnitude > 0f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            orientation.rotation = Quaternion.Slerp(orientation.rotation, lookRotation, rotationSpeed);
        }
    }

    public void CalculateVelocity()
    {
        Vector3 targetVelocity = moveDirection * moveSpeed;

        currentVelocityX = Mathf.SmoothDamp(currentVelocityX, targetVelocity.x, ref currentVelocityXRef, velocitySmoothTime);
        currentVelocityY = Mathf.SmoothDamp(currentVelocityY, targetVelocity.y, ref currentVelocityYRef, velocitySmoothTime);

        rb.velocity = new Vector2(currentVelocityX, currentVelocityY);
    }

    public void SetMovementDirection(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        SetMovementDirection(context.ReadValue<Vector2>().normalized);
    }
}
