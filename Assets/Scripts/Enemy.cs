using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        // Only add if the manager actually exists in the scene
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.AddEnemy(this);
        }
    }

    void OnDestroy()
    {
        // Only remove if the manager hasn't already been destroyed by a scene change
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.RemoveEnemy(this);
        }
    }

    void Update()
    {
        
    }
}