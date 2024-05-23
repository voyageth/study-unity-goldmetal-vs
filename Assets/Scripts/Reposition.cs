using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D collider2d;

    private void Awake()
    {
        collider2d = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 tilemapPosition = transform.position;
        float diffX = Mathf.Abs(playerPosition.x - tilemapPosition.x);
        float diffY = Mathf.Abs(playerPosition.y - tilemapPosition.y);

        Vector3 playerDirection = GameManager.Instance.player.inputVector;
        float directionX = playerDirection.x < 0 ? -1 : 1;
        float directionY = playerDirection.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * directionX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * directionY * 40);
                }
                break;
            case "Enemy":
                if (collider2d.enabled)
                {
                    transform.Translate(playerDirection * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}
