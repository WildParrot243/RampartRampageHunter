using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(order = 11, menuName = "RampartRampage/EnemyStats", fileName = "EnemyStats")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] private float damage;
    [field: SerializeField] private float maxHealth;
    [field: SerializeField] private float value;
    [field: SerializeField] private float animationSpeed;
    [field: SerializeField] private float speed;

    public float Damage => damage * Stats.gameStats.multilperDamage;
    public float MaxHealth => maxHealth * Stats.gameStats.multilperHealth;
    public float Value => value * Stats.gameStats.multilperValue;
    public float AnimationSpeed => animationSpeed * Stats.gameStats.multilperSpeed;
    public float Speed => speed * Stats.gameStats.multilperSpeed;


}
