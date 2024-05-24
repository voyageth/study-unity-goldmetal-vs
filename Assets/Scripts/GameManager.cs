using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTimeInSecond;
    public float maxGameTimeInSeccond = 20f;

    public PoolManager poolManager;
    public Player player;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTimeInSecond += Time.deltaTime;

        if (gameTimeInSecond > maxGameTimeInSeccond)
        {
            gameTimeInSecond = 0;
        }
    }
}
