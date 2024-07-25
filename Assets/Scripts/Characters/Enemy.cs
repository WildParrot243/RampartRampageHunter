using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class Enemy : MonoBehaviour, IDamageable
{
    private float CurrentSinkTime;
    [SerializeField] protected EnemySO stats;
    [SerializeField] private Image healthBar;
    private float _health;
    protected RagdollCtrl ragdollCtrl;
    public static Action<Enemy> OnDefeated;

    protected Animator animation;
    protected Rigidbody rb;
    protected AudioSource sound;
    public static int totalEnemies {  get; private set; }
    private bool isDead;
    public float health { get => _health; set {
            _health = value;
            if(healthBar) healthBar.fillAmount = health / maxHealth;
        }
    }

    public float maxHealth { get => stats.MaxHealth; set => throw new NotImplementedException(); }

    public void Die()
    {
        CurrentSinkTime = Mathf.Min(5, CurrentSinkTime + 1);
        if (isDead) return;
        isDead = true;
        // Destroy(gameObject);
      
        ragdollCtrl.BeginRagdoll();
        if (healthBar)
        healthBar.transform.parent.gameObject.SetActive(false);
        OnDefeated.Invoke(this);
        StartCoroutine(GoThroughGround());
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

    private IEnumerator GoThroughGround()
    {
        while (CurrentSinkTime > 0)
        {
            CurrentSinkTime -= Time.deltaTime;
            yield return null;
        }

        rb.isKinematic = true;
        ragdollCtrl.FreezeRagdoll();
        while (transform.position.y >= 0)
        {
            transform.position += Vector3.down * (Time.deltaTime * 3);
            yield return null;

        }

        Destroy(gameObject);
    }
}
