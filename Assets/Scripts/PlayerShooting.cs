using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Min(0)] public int bulletsAmount = 30;
    public GameObject prefab;
    public GameObject shootPoint;
    public ParticleSystem muzzleEffect;
    public AudioSource shootingAudioSource;

    public void OnFire(InputValue value)
    {
        if (!value.isPressed || Time.timeScale <= 0f || bulletsAmount <= 0)
        {
            return;
        }

        if (prefab == null || shootPoint == null)
        {
            return;
        }

        Instantiate(prefab, shootPoint.transform.position, shootPoint.transform.rotation);
        bulletsAmount--;
        if (shootingAudioSource != null && shootingAudioSource.clip != null)
        {
            shootingAudioSource.PlayOneShot(shootingAudioSource.clip);
        }
        if (muzzleEffect != null)
        {
            muzzleEffect.Play();
        }
    }

}
