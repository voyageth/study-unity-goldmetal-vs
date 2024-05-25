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

    float timer;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

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

    public void Init()
    {
        switch (weaponType)
        {
            case WeaponType.WEAPON_0:
                speed = 150;
                ActivateWeapon0();
                break;
            default:
                speed = 0.3f;
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (weaponType == WeaponType.WEAPON_0)
        {
            ActivateWeapon0();
        }
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
                bulletTransform = GameManager.instance.poolManager.Get(prefabType).transform;
                bulletTransform.parent = transform;
            }

            // reposition
            bulletTransform.localPosition = Vector3.zero;
            bulletTransform.localRotation = Quaternion.identity;
            bulletTransform.Rotate(Vector3.forward * 360 * index / count);
            bulletTransform.Translate(bulletTransform.up * 1.5f, Space.World);

            // init
            bulletTransform.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 = Inf
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
    }
}
