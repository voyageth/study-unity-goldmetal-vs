using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    public ItemData.WeaponType weaponType;
    public PrefabType prefabType;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    private void Awake()
    {
        player = GameManager.instance.player;
    }

    private void Start()
    {
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (weaponType)
        {
            case ItemData.WeaponType.WEAPON_0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                timer += Time.deltaTime;
                if (timer > (3/(float)count))
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Melee);
                    timer = 0f;
                }
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    Fire();
                    timer = 0f;
                }
                break;
        }

        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    public void Init(ItemData itemData)
    {
        // Basic Set
        name = "Weapon " + itemData.itemType;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        damage = itemData.baseDamage * Character.Damage;
        count = itemData.baseCount + Character.Count;
        weaponType = itemData.weaponType;
        prefabType = itemData.prefabType;

        switch (weaponType)
        {
            case ItemData.WeaponType.WEAPON_0:
                speed = 150 * Character.WeaponSpeed;
                ActivateWeapon0();
                break;
            default:
                speed = 0.3f * Character.WeaponRate;
                break;
        }

        // Hand Set
        Hand hand = null;
        switch(itemData.itemType)
        {
            case ItemData.ItemType.Melee:
                hand = player.hands[0];
                break;
            case ItemData.ItemType.Range:
                hand = player.hands[1];
                break;
        }
        if (hand != null)
        {
            hand.spriteRenderer.sprite = itemData.hand;
            hand.gameObject.SetActive(true);
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage;
        this.count += count;

        if (weaponType == ItemData.WeaponType.WEAPON_0)
        {
            ActivateWeapon0();
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }


    private void ActivateWeapon0()
    {
        for (int index = 0; index < count; index++)
        {
            // get or create bullet
            Transform bulletTransform;
            if (index < transform.childCount)
            {
                bulletTransform = transform.GetChild(index);
            }
            else
            {
                bulletTransform = GameManager.instance.poolManager.Get(PrefabType.BULLET_0).transform;
                bulletTransform.parent = transform;
            }

            // reposition
            bulletTransform.localPosition = Vector3.zero;
            bulletTransform.localRotation = Quaternion.identity;
            bulletTransform.Rotate(Vector3.forward * 360 * index / count);
            bulletTransform.Translate(bulletTransform.up * 1.5f, Space.World);

            // init
            bulletTransform.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 = Inf
        }
    }

    private void Fire()
    {
        Transform nearestTarget = player.scanner.nearestTarget;
        if (nearestTarget == null)
            return;

        Transform bulletTransform = GameManager.instance.poolManager.Get(PrefabType.BULLET_1).transform;
        
        // reposition
        Vector3 targetPosition = nearestTarget.position;
        Vector3 direction = targetPosition - transform.position;
        direction = direction.normalized;
        bulletTransform.position = transform.position;
        bulletTransform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

        // init
        bulletTransform.GetComponent<Bullet>().Init(damage, count, direction);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
