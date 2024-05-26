using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public enum HandType
    {
        LEFT,
        RIGHT,
    }
    public HandType handType;
    public SpriteRenderer spriteRenderer;

    SpriteRenderer playerSpriteRenderer;

    Vector3 rightHandPosition = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightHandPositionReverse = new Vector3(-0.35f, -0.15f, 0);

    Quaternion leftHandRotate = Quaternion.Euler(0, 0, -35);
    Quaternion leftHandRotateReverse = Quaternion.Euler(0, 0, -135);

    private void Awake()
    {
        playerSpriteRenderer = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReverse = playerSpriteRenderer.flipX;

        switch(handType)
        {
            case HandType.LEFT:
                // 근접 무기
                transform.localRotation = isReverse ? leftHandRotateReverse : leftHandRotate;
                spriteRenderer.flipY = isReverse;
                spriteRenderer.sortingOrder = isReverse ? 4 : 6;
                break;
            case HandType.RIGHT:
                // 원거리 무기
                transform.localPosition= isReverse ? rightHandPositionReverse : rightHandPosition;
                spriteRenderer.flipX = isReverse;
                spriteRenderer.sortingOrder = isReverse ? 6 : 4;
                break;
        }
    }
}
