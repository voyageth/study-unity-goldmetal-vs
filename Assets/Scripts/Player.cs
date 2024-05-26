using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVector;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;

    Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        scanner = GetComponent<Scanner>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void FixedUpdate()
    {
        // move
        Vector2 nextVector = inputVector * speed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + nextVector);
    }

    void OnMove(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
    }

    void OnFire(InputValue inputValue)
    {
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVector.magnitude);
        if (inputVector.x !=0)
            spriteRenderer.flipX = inputVector.x < 0;
    }
}
