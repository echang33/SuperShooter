using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;
    public ParticleSystem muzzleEffect;
    public AudioSource shootingAudioSource;

    public void OnFire(){
        GameObject clone = Instantiate(prefab, shootPoint.transform.position, shootPoint.transform.rotation);
        if (shootingAudioSource != null && shootingAudioSource.clip != null)
        {
            shootingAudioSource.PlayOneShot(shootingAudioSource.clip);
        }
        muzzleEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
