using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState
    {
        GoToBase, AttackBase, ChasePlayer, AttackPlayer
    }

    public EnemyState currentState;
    public Sight sightSensor;
    public Transform baseTransform;
    public float baseAttackDistance;
    public float playerAttackDistance;
    private UnityEngine.AI.NavMeshAgent agent;
    public GameObject bulletPrefab;
    public GameObject shootPoint;
    public float lastShootTime;
    public float fireRate = 2f; // Shoots once every 2 seconds

    void Awake()
    {
        GameObject target = GameObject.Find("attackPoint");

        if (target != null)
        {
            baseTransform = target.transform;
        }
        else
        {
            Debug.LogWarning("Could not find the attackPoint in the scene!");
        }

        agent = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        // Safety check: Prevent crash if the Inspector slots are empty or agent wasn't found
        if (agent == null || sightSensor == null) return;

        switch (currentState)
        {
            case EnemyState.GoToBase:
                GoToBase();
                break;
            case EnemyState.AttackBase:
                AttackBase();
                break;
            case EnemyState.ChasePlayer:
                ChasePlayer();
                break;
            case EnemyState.AttackPlayer:
                AttackPlayer();
                break;
        }
    }

    

    void Shoot()
    {
        if (bulletPrefab != null && shootPoint != null)
        {
            var timeSinceLastShot = Time.time - lastShootTime;
            if (timeSinceLastShot >= fireRate)
            {
                lastShootTime = Time.time;
                GameObject clone = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            }
        }
        else
        {
            Debug.LogWarning("Bullet prefab or shoot point is not assigned!");
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.position);
        directionToPosition.y = 0; // Keep the enemy upright
        transform.parent.forward = directionToPosition;
    }

    void GoToBase()
    {
        agent.isStopped = false;        
        
        // Safety guard: Only try to move if the base was actually found
        if (baseTransform != null)
        {
            agent.SetDestination(baseTransform.position);
            
            float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);
            if (distanceToBase <= baseAttackDistance)
            {
                currentState = EnemyState.AttackBase;
            }
        }

        if (sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    void AttackBase()
    {
        agent.isStopped = true;
    
        if (baseTransform == null) return;

        LookTo(baseTransform.position);
        Shoot();
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        agent.SetDestination(sightSensor.detectedObject.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.AttackPlayer;
        }
    }

    void AttackPlayer()
    {
        agent.isStopped = true;
        
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        LookTo(sightSensor.detectedObject.transform.position);
        Shoot();
        
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }
    
}