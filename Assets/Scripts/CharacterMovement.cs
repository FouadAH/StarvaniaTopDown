using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float moveSpeed = 3f;
    [Header("Acceleration Settings")]
    public float accelarationGrounded = 0.05f;

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
        Vector3 targetVelocity = moveDirection * moveSpeed;

        currentVelocityX = Mathf.SmoothDamp(currentVelocityX, targetVelocity.x, ref currentVelocityXRef, accelarationGrounded);
        currentVelocityY = Mathf.SmoothDamp(currentVelocityY, targetVelocity.y, ref currentVelocityYRef, accelarationGrounded);

        rb.velocity = new Vector2(currentVelocityX, currentVelocityY);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().normalized;
    }
}
