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

        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 tilemapPosition = transform.position;

        switch (transform.tag)
        {
            case "Ground":
                float diffX = playerPosition.x - tilemapPosition.x;
                float diffY = playerPosition.y - tilemapPosition.y;
                float directionX = diffX < 0 ? -1 : 1;
                float directionY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);


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
                    Vector3 dist = playerPosition - tilemapPosition;
                    Vector3 randomVector = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0); 
                    transform.Translate(randomVector + dist * 2);
                }
                break;
        }
    }
}
