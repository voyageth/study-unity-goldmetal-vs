using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform levelUpRectTransform;
    Item[] items;

    private void Awake()
    {
        levelUpRectTransform = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        levelUpRectTransform.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        levelUpRectTransform.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        // ��� ������ ��Ȱ��ȭ
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // �� �߿��� ���� 3�� ������ Ȱ��ȭ
        int[] random = new int[3];
        while(true)
        {
            random[0] = Random.Range(0, items.Length);
            random[1] = Random.Range(0, items.Length);
            random[2] = Random.Range(0, items.Length);

            if (random[0] != random[1] && random[1] != random[2] && random[1] != random[2])
                break;
        }

        for (int index =0; index < random.Length; index++)
        {
            Item randomItem = items[random[index]];
            if (randomItem.level == randomItem.itemData.damages.Length)
            {
                // ���� �������� ���� �Һ� ���������� ��ü
                items[4].gameObject.SetActive(true);
            }
            else
            {
                randomItem.gameObject.SetActive(true);
            }
        }
    }
}
