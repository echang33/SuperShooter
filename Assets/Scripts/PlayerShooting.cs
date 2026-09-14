using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnFire(){
        GameObject clone = Instantiate(prefab, shootPoint.transform.position, shootPoint.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
