using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float startTime;
    [SerializeField] private float endTime;
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        WaveManager.instance.AddWave(this);
        InvokeRepeating("Spawn", startTime, spawnRate);
        Invoke("EndSpawner", endTime);
    }

    void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    void EndSpawner()
    {
        WaveManager.instance.RemoveWave(this);
        CancelInvoke("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
