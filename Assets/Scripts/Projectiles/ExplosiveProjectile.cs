using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
   [SerializeField] private float Radius;
    private readonly Collider[] collisions = new Collider[10]; 
    protected override void OnHit(Collision collision)
    {
        Vector3 pos = transform.position + Vector3.up;
        int num = Physics.OverlapSphereNonAlloc(pos, Radius, collisions, StaticUtilites.EnemyLayerID);

        for (int i = 0; i < num; i++)
        {
            Collider current = collisions[i];
            Debug.DrawLine(pos, current.transform.position, Color.red, 3);
            if (Physics.Raycast(pos, current.transform.position - pos, out RaycastHit hit, Radius, StaticUtilites.BlockingLayers ))
            {
                if (hit.rigidbody && hit.rigidbody.TryGetComponent(out IDamageable e))
                {
                    e.TakeDamage(maxDamage);

                }
            }
        } Destroy(gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
