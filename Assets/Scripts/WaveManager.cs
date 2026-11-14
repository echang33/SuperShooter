using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public List<WaveSpawner> waves = new List<WaveSpawner>();

    public UnityEvent onWaveComplete;
    public UnityEvent onChanged = new UnityEvent();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Duplicated WavesManager", gameObject);
        }
    }

    public void AddWave(WaveSpawner wave)
    {
        waves.Add(wave);
        onChanged.Invoke();
        if (onWaveComplete != null)
        {
            onWaveComplete.Invoke();
        }
    }

    public void RemoveWave(WaveSpawner wave)
    {
        waves.Remove(wave);
        onChanged.Invoke();
        if (onWaveComplete != null)
        {
            onWaveComplete.Invoke();
        }
    }
}
