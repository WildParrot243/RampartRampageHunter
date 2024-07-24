using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] private Transform Target;
    public static Vector3 pos { get; private set; }
    private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        pos = Target.position;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb && rb.TryGetComponent(out Enemy e)) 
        {
            TakeDamage(e.GetDamage());
            Destroy(e.gameObject);
        }

    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // We Lost Die();
            print("We Lost");
        }

        healthBar.fillAmount = currentHealth / maxHealth;
        
    }

}
