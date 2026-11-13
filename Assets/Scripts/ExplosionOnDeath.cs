using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnDeath : MonoBehaviour
{
    public GameObject particlePrefab;

    public void Awake(){
        var life = GetComponent<Life>();
        life.onDeath.AddListener(OnDeath);
    }

    void OnDeath(){
        Vector3 rotation = transform.eulerAngles;
        rotation.x = -90f;
        Instantiate(particlePrefab, transform.position, Quaternion.Euler(rotation));
    }
}
