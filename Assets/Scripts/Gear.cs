using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType itemType;
    public float rate;

    public void Init(ItemData itemData)
    {
        // Basic Set
        name = "Gear" + itemData.itemType;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property set
        itemType = itemData.itemType;
        rate = itemData.damages[0];

        // apply
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;

        // apply
        ApplyGear();
    }

    void ApplyGear()
    {
        switch(itemType)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = GameManager.instance.player.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in weapons)
        {
            switch(weapon.weaponType)
            {
                case ItemData.WeaponType.WEAPON_0:
                    float speed = 150 * Character.WeaponSpeed;
                    weapon.speed = speed + (speed * rate);
                    break;
                case ItemData.WeaponType.WEAPON_1:
                    speed = 0.5f * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 3 * Character.Speed;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
