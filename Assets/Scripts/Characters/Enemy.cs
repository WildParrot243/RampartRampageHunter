using System;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemySO stats;
    [SerializeField] private Image healthBar;
    private float _health;
    protected RagdollCtrl ragdollCtrl;
    public static Action<Enemy> OnDefeated;

    protected Animator animation;
    protected Rigidbody rb;
    protected AudioSource sound;
    public static int totalEnemies {  get; private set; }
    
    public float health { get => _health; set {
            _health = value;
            if(healthBar) healthBar.fillAmount = health / maxHealth;
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
        sound = GetComponentInChildren<AudioSource>();
        ragdollCtrl = GetComponentInChildren<RagdollCtrl>();

        
        animation.speed = stats.AnimationSpeed;
        totalEnemies += 1;
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
