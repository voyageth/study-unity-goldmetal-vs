using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTimeInSecond;
    public float maxGameTimeInSeccond = 20f;
    
    [Header("# GameObject")]
    public PoolManager poolManager;
    public Player player;

    [Header("# Player Info")]
    public int health;
    public int maxHealth;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        gameTimeInSecond += Time.deltaTime;

        if (gameTimeInSecond > maxGameTimeInSeccond)
        {
            gameTimeInSecond = 0;
        }
    }

    public void KillEnemy()
    {
        kill++;

        if (++exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }

    public float GetExpPercentage()
    {
        return ((float)exp) / nextExp[level];
    }

    public (int, int) GetRemainTime()
    {
        float remainTime =  maxGameTimeInSeccond - gameTimeInSecond;
        int min = Mathf.FloorToInt(remainTime / 60);
        int second = Mathf.FloorToInt(remainTime % 60);
        return (min, second);
    }

    public float GetHealthPercentage()
    {
        return ((float)health) / maxHealth;
    }
}
