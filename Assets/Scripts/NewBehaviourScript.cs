using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float maxHealth { get; set; }
    public float health { get; set; }

    public void Die()
    {
        print("BOXDEAD");
    }
}
