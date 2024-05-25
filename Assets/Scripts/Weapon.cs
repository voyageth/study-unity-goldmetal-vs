using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    WEAPON_0,
    WEAPON_1,
}

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType;
    public PrefabType prefabType;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }

    void Update()
    {
        switch (weaponType)
        {
            case WeaponType.WEAPON_0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    public void Init()
    {
        switch (weaponType)
        {
            case WeaponType.WEAPON_0:
                speed = 150;
                Fire();
                break;
            default:
                break;
        }
    }


    private void Fire()
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
                bulletTransform = GameManager.instance.poolManager.Get(prefabType).transform;
                bulletTransform.parent = transform;
            }

            // reposition
            bulletTransform.localPosition = Vector3.zero;
            bulletTransform.localRotation = Quaternion.identity;
            bulletTransform.Rotate(Vector3.forward * 360 * index / count);
            bulletTransform.Translate(bulletTransform.up * 1.5f, Space.World);

            // init
            bulletTransform.GetComponent<Bullet>().Init(damage, -1); // -1 = Inf
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (weaponType == WeaponType.WEAPON_0)
        {
            Fire();
        }
    }
}
