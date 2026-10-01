using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<Enemy> enemies = new List<Enemy>();

    public UnityEvent OnEnemyDeath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath.Invoke();
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath.Invoke();
        }
    }   

}
