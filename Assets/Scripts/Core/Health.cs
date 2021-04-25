using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float healthPoints;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        healthPoints = Mathf.Max(0, healthPoints -= damage);

        if (healthPoints == 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
