using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    public int amount;

    public void givePoints()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.amount += amount;
        }
    }

    void Awake()
    {
        var life = GetComponent<Life>();
        life.onDeath.AddListener(givePoints);
    }

    void OnDestroy()
    {
        var life = GetComponent<Life>();
        life.onDeath.RemoveListener(givePoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
