using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PrefabTypeGameObjectPair
{
    public PrefabType key;
    public GameObject value;
}

public enum PrefabType
{
    ENEMY_A,
    ENEMY_B,
    ENEMY_C,
    ENEMY_D,
    ENEMY_E,
}

static class EnumCache<T> where T : Enum
{
    private static readonly T[] Values = (T[])Enum.GetValues(typeof(T));

    public static T GetRandomValue()
    {
        return Values[UnityEngine.Random.Range(0, Values.Length)];
    }
}


public class PoolManager : MonoBehaviour
{
    public List<PrefabTypeGameObjectPair> prefabList;

    Dictionary<PrefabType, GameObject> prefabDictionary;
    Dictionary<PrefabType, List<GameObject>> prefabPoolDictionary;


    private void Awake()
    {
        prefabDictionary = new Dictionary<PrefabType, GameObject>();
        prefabPoolDictionary = new Dictionary<PrefabType, List<GameObject>>();

        foreach (PrefabTypeGameObjectPair pair in prefabList)
        {
            prefabDictionary.Add(pair.key, pair.value);
            prefabPoolDictionary.Add(pair.key, new List<GameObject>());
        }
    }

    public GameObject Get(PrefabType prefabType)
    {
        if (!prefabPoolDictionary.ContainsKey(prefabType) || !prefabDictionary.ContainsKey(prefabType))
        {
            throw new Exception("Unregistered prefabType : " + prefabType);
        }

        List<GameObject> gameObjects = prefabPoolDictionary[prefabType];

        foreach(GameObject item in gameObjects)
        {
            if (!item.activeSelf)
            {
                item.SetActive(true);
                return item;
            }
        }

        GameObject newItem = Instantiate(prefabDictionary[prefabType]);
        gameObjects.Add(newItem);
        return newItem;
    }

    public PrefabType GetRandomPrefabType()
    {
        return EnumCache<PrefabType>.GetRandomValue();
    }

    public GameObject GetRandom()
    {
        return Get(GetRandomPrefabType());
    }
}
