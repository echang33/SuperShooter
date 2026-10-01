using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WavesGameMode : MonoBehaviour
{
    [SerializeField] Life playerLife;
    [SerializeField] Life playerBaseLife;

    void Start()
    {
        if (playerLife != null)
        {
            playerLife.onDeath.AddListener(OnPlayerOrBaseDied);
        }
        if (playerBaseLife != null)
        {
            playerBaseLife.onDeath.AddListener(OnPlayerOrBaseDied);
        }
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.OnEnemyDeath.AddListener(CheckWinCondition);
        }
        if (WaveManager.instance != null)
        {
            WaveManager.instance.onWaveComplete.AddListener(CheckWinCondition);
        }
    }

    void CheckWinCondition()
    {
        if (EnemyManager.instance != null && WaveManager.instance != null)
        {
            if (EnemyManager.instance.enemies.Count <= 0 && WaveManager.instance.waves.Count <= 0)
            {
                SceneManager.LoadScene("Scenes/WinScreen");
            }
        }
    }

    void OnPlayerOrBaseDied()
    {
        SceneManager.LoadScene("Scenes/LoseScreen");
    }
}