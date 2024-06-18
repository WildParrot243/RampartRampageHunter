using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[SelectionBase]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float collectedGems;
    [SerializeField] private Image healthBar;
    private float _health;
    private RagdollCtrl ragdollCtrl;
    public static Action<Enemy> OnDefeated;

    protected Animator animation;
    protected Rigidbody rb;
    protected NavMeshAgent movement;
    protected AudioSource sound;

    [field:SerializeField ]public float maxHealth { get; set; }
    [field: SerializeField] public float damage { get; set; }
    public float health { get => _health; set {

            _health = value;
            healthBar.fillAmount = health / maxHealth;
        }
    }

    public void Die()
    {
        print("DEAD");
        // Destroy(gameObject);
        ragdollCtrl.BeginRagdoll();
        healthBar.transform.parent.gameObject.SetActive(false);
        OnDefeated.Invoke(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
      
        animation = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<NavMeshAgent>();
        sound = GetComponentInChildren<AudioSource>();
        ragdollCtrl = GetComponentInChildren<RagdollCtrl>();
        movement.SetDestination(Castle.pos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCollectedGems()
    {
        return collectedGems;
    }
}
