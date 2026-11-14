using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Min(0)] public int bulletsAmount = 30;
    public GameObject prefab;
    public GameObject shootPoint;
    public ParticleSystem muzzleEffect;
    public AudioSource shootingAudioSource;
    [SerializeField] private Animator animator;
    private static readonly int ShootParameter = Animator.StringToHash("Shoot");
    private static readonly int ShootState = Animator.StringToHash("Shoot_SingleShot_AR");
    private bool shotPending;

    void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (muzzleEffect != null)
        {
            // This is a reusable effect attached to the player.
            var main = muzzleEffect.main;
            main.stopAction = ParticleSystemStopAction.None;
            main.playOnAwake = false;
            muzzleEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    void LateUpdate()
    {
        UpdateMuzzlePose();
        if (!shotPending || Time.timeScale <= 0f || animator == null)
        {
            return;
        }

        var state = animator.GetCurrentAnimatorStateInfo(0);
        // LateUpdate runs after the Animator has applied the raised-gun pose.
        if (state.shortNameHash == ShootState && !animator.IsInTransition(0)
            && state.normalizedTime >= 0.15f)
        {
            shotPending = false;
            FireShot();
        }
    }

    void OnDisable()
    {
        shotPending = false;
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            animator.ResetTrigger(ShootParameter);
        }
    }

    void UpdateMuzzlePose()
    {
        if (muzzleEffect != null && shootPoint != null)
        {
            muzzleEffect.transform.SetPositionAndRotation(
                shootPoint.transform.position, transform.rotation);
        }
    }

    public void OnFire(InputValue value)
    {
        if (!value.isPressed || Time.timeScale <= 0f || bulletsAmount <= 0 || shotPending)
        {
            return;
        }

        if (prefab == null || shootPoint == null)
        {
            return;
        }

        if (animator != null && animator.isActiveAndEnabled
            && animator.runtimeAnimatorController != null)
        {
            shotPending = true;
            animator.SetTrigger(ShootParameter);
        }
        else
        {
            FireShot();
        }
    }

    void FireShot()
    {
        if (Time.timeScale <= 0f || bulletsAmount <= 0 || prefab == null || shootPoint == null)
        {
            return;
        }

        // Aim follows the player's facing direction, independent of animated gun bones.
        Instantiate(prefab, shootPoint.transform.position, transform.rotation);
        bulletsAmount--;
        if (shootingAudioSource != null && shootingAudioSource.clip != null)
        {
            shootingAudioSource.PlayOneShot(shootingAudioSource.clip);
        }
        if (muzzleEffect != null)
        {
            UpdateMuzzlePose();
            // Restart the burst even when the previous shot's particles are still alive.
            muzzleEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            muzzleEffect.Play(true);
        }
    }

}
