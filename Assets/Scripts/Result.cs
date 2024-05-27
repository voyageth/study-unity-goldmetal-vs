using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject loseObject;
    public GameObject winObject;

    public void Lose()
    {
        loseObject.SetActive(true);
    }
    
    public void Win()
    {
        winObject.SetActive(true);
    }
}
