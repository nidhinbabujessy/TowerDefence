using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents eventss;

    private void Awake()
    {
        eventss = this;
    }


    public event Action bulletHit;

    public void bullethiting()
    {
        bulletHit();
    }
}
