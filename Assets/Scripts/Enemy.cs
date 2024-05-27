using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] runtimeAnimatorController;
    public Rigidbody2D target;

    bool isLive;
    Rigidbody2D enemyRigidbody;
    Collider2D enemyCollider;
    Animator animator;
    SpriteRenderer spriteRenderer;
    WaitForFixedUpdate waitForKnockBack;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        waitForKnockBack = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 directionVector = target.position - enemyRigidbody.position;
        Vector2 nextVector = directionVector.normalized * speed * Time.fixedDeltaTime;

        enemyRigidbody.MovePosition(enemyRigidbody.position + nextVector);
        enemyRigidbody.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive)
            return;
        
        spriteRenderer.flipX = target.position.x < enemyRigidbody.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        enemyCollider.enabled = true;
        enemyRigidbody.simulated = true;
        spriteRenderer.sortingOrder = 2;
        animator.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = runtimeAnimatorController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        Bullet bullet = collision.GetComponent<Bullet>();
        health -= bullet.damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            // hit action
            animator.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            enemyCollider.enabled = false;
            enemyRigidbody.simulated = false;
            spriteRenderer.sortingOrder = 1;
            animator.SetBool("Dead", true);
            GameManager.instance.KillEnemy();
        }
    }

    IEnumerator KnockBack()
    {
        yield return waitForKnockBack; // 하나의 물리 프레임 딜레이
        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 direction = transform.position - playerPosition;
        enemyRigidbody.AddForce(direction.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
