using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable


{

    public float maxHealth { get; set; }
    public float health { get; set; }

    public void TakeDamage(float Damage) 
    {
        if (health <= 0)
            return;
            health -= Damage;   
        if (health <= 0)
        {

            Die();
        }
    }

    public void Die();


}
