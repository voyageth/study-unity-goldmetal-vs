using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{   public enum WeaponType
    {
        NONE,
        WEAPON_0,
        WEAPON_1,
    }

    public enum ItemType
    {
        Melee,
        Range,
        Glove,
        Shoe,
        Heal,
    }

    [Header("# Main Info")]
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDescription;
    public Sprite itemIcon;
    public ItemType itemType;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public WeaponType weaponType;
    public PrefabType prefabType;
    public Sprite hand;
}
