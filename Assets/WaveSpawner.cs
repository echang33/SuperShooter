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
        InvokeRepeating("Spawn", startTime, spawnRate);
        Invoke("CancelInvoke", endTime);
    }

    void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    void cancelInvoke()
    {
        CancelInvoke("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
