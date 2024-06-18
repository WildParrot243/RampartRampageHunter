using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rewardSysterm : MonoBehaviour
{
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = transform.GetChild(0).GetComponent<ParticleSystem>();
        Enemy.OnDefeated += SpawnParticle;
    }

   void SpawnParticle(float enemyvalue, Vector3 location)
    {
        particles.emission.SetBurst(0, new ParticleSystem.Burst(0, (short)enemyvalue));
        transform.position = location;
        particles.Play();
    }

    void SpawnParticle(Enemy enemy)
    {
        SpawnParticle(enemy.GetCollectedGems(), enemy.transform.position);
    }

    private void OnDestroy()
    {
        Enemy.OnDefeated -= SpawnParticle;
    }
}
