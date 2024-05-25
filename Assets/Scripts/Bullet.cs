using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D bulletRigidBody;

    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 direction)
    {
        this.damage = damage;
        this.per = per;

        if (per >= 0)
        {
            bulletRigidBody.velocity = direction * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per < 0)
            return;

        per--;

        if (per == -1)
        {
            bulletRigidBody.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }

    }
}
