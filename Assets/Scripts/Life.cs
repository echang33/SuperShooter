using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{

    public float amount;
    public float MaxAmount { get; private set; }
    public UnityEvent onDeath;

    void Awake()
    {
        MaxAmount = Mathf.Max(0f, amount);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (amount <= 0)
        {
            if (onDeath != null)
            {
                onDeath.Invoke();
            }

            Destroy(gameObject);
        }
    }
    


}
