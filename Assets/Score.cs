using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text Coins;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        GameEvents.eventss.bulletHit += add;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void add()
    {
        count=count+10;
        Coins.text =count.ToString();
    }
}
