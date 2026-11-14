using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator animator;
    private static readonly int VelocityParameter = Animator.StringToHash("Velocity");
    private Vector2 movementValue;
    private float lookValue;
    private Rigidbody rb;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (animator != null)
        {
            // Movement stays controlled by this script rather than the animation.
            animator.applyRootMotion = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>() * speed;
    }

    public void OnLook(InputValue value)
    {
        lookValue = value.Get<Vector2>().x * rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            animator.SetFloat(VelocityParameter, movementValue.magnitude);
        }
        transform.Translate(movementValue.x * Time.deltaTime, 0, movementValue.y * Time.deltaTime);
        transform.Rotate(0, lookValue * Time.deltaTime, 0);
        //rb.AddRelativeForce(movementValue.x * Time.deltaTime, 0, movementValue.y * Time.deltaTime);
        //rb.AddTorque(0, lookValue * Time.deltaTime, 0);
    }


}
