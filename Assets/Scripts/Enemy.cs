using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 directionVector = target.position - rigidbody2D.position;
        Vector2 nextVector = directionVector.normalized * speed * Time.fixedDeltaTime;

        rigidbody2D.MovePosition(rigidbody2D.position + nextVector);
        rigidbody2D.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;
        
        spriteRenderer.flipX = target.position.x < rigidbody2D.position.x;
    }
}
