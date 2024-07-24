using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(order = 10, menuName = "RampartRampage/ProjectileStats", fileName = "ProjectileStats")]
public class ProjectileSO : ScriptableObject
{
    [field: SerializeField] public float minDamage { get; private set; }
    [field: SerializeField] public float maxDamage { get; private set; }
    [field: SerializeField] public float minSpeed { get; private set; }
    [field: SerializeField] public float maxSpeed {  get; private set; }
    [field: SerializeField] public AudioClip impactSound {  get; private set; }
    [field: SerializeField] public ParticleSystem impactEffect { get; private set; }
    [field: SerializeField] public float explosionRadius { get; private set; }

}
