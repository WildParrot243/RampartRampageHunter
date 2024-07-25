using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public interface IDamageable


{

    public float maxHealth { get; set; }
    public float health { get; set; }

    public void TakeDamage(float Damage) 
    {
        
            health -= Damage;   
        if (health < 0)
        {

            Die();


        }
    }

    public void Die();


}
