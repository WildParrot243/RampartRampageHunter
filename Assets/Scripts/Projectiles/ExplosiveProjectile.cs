using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{

    private readonly Collider[] collisions = new Collider[10]; 
    protected override void OnHit(Collision collision)
    {
        Vector3 pos = transform.position + Vector3.up;
        int num = Physics.OverlapSphereNonAlloc(pos, stats.explosionRadius, collisions, StaticUtilites.EnemyLayerID);

        for (int i = 0; i < num; i++)
        {
            Collider current = collisions[i];
            Debug.DrawLine(pos, current.transform.position, Color.red, 3);
            if (Physics.Raycast(pos, current.transform.position - pos, out RaycastHit hit, stats.explosionRadius, StaticUtilites.BlockingLayers ))
            {
                if (hit.rigidbody && hit.rigidbody.TryGetComponent(out IDamageable e))
                {
                    e.TakeDamage(stats.maxDamage);

                }
            }
        } Destroy(gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, stats.explosionRadius);
    }
}
