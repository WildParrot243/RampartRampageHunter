using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[SelectionBase]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemySO stats;
    [SerializeField] private Image healthBar;
    private float _health;
    private RagdollCtrl ragdollCtrl;
    public static Action<Enemy> OnDefeated;

    protected Animator animation;
    protected Rigidbody rb;
    protected NavMeshAgent movement;
    protected AudioSource sound;
    public static int totalEnemies {  get; private set; }
    
    public float health { get => _health; set {

            _health = value;
            healthBar.fillAmount = health / stats.MaxHealth;
        }
    }

    public float maxHealth { get => stats.MaxHealth; set => throw new NotImplementedException(); }

    public void Die()
    {
        print("DEAD");
        // Destroy(gameObject);
      
        ragdollCtrl.BeginRagdoll();
        healthBar.transform.parent.gameObject.SetActive(false);
        OnDefeated.Invoke(this);
    }

    void Awake()
    {
        health = stats.MaxHealth;

        animation = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<NavMeshAgent>();
        sound = GetComponentInChildren<AudioSource>();
        ragdollCtrl = GetComponentInChildren<RagdollCtrl>();

        movement.speed = stats.Speed;
        animation.speed = stats.AnimationSpeed;
        totalEnemies += 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        movement.SetDestination(Castle.pos);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCollectedGems()
    {
        return stats.Value;
    }

    public float GetDamage()
    { 
        return stats.Damage; 
    }

    private void OnDestroy()
    {
        if (!Application.isPlaying) return;
        totalEnemies -= 1;
        if (totalEnemies <= 0)
        {
            totalEnemies = 0;
            GameManager.Instance.OnRoundEnd();
        }
    }
}
