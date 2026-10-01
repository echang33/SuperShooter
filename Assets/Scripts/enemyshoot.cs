using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;
    
    // Add a fire rate to control how fast the enemy shoots
    public float fireRate; // Shoots once every 2 seconds
    private float timer = 0f;

    void Start()
    {
        // Optional: Start the timer at a random value so multiple enemies don't all shoot on the exact same frame
        timer = Random.Range(0f, fireRate);
    }

    void Update()
    {
        // Time.deltaTime adds the exact fraction of a second that has passed since the last frame
        timer += Time.deltaTime;

        // If enough time has passed, shoot and reset the timer
        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    // We moved the instantiation into its own method to keep things clean
    void Shoot()
    {
        GameObject clone = Instantiate(prefab, shootPoint.transform.position, shootPoint.transform.rotation);
    }
}