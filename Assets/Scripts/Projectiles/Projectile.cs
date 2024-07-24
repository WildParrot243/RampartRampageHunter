using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileSO stats;
    float charge;
    Rigidbody rb;
    TrailRenderer tr;
    Collider[] colliders;
    AudioSource AS;

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision);
        if (AS && stats.impactSound)
        AS.PlayOneShot(stats.impactSound, 1);
        if (stats.impactEffect)
        {
            ParticleSystem PS = (Instantiate(stats.impactEffect, transform.position, Quaternion.identity));
            Destroy(PS.gameObject, PS.main.duration);
        }
    }

    protected virtual void OnHit(Collision collision)
    {
        print(collision.gameObject.name + ", " + Mathf.Lerp(stats.minDamage, stats.maxDamage, charge));
        
        if (collision.rigidbody && collision.rigidbody.TryGetComponent(out IDamageable e))
        {
            e.TakeDamage(Mathf.Lerp(stats.minDamage, stats.maxDamage, charge));

        }
        transform.parent = collision.transform;
        rb.isKinematic = true;
        Destroy(gameObject, 15);

    }


    public void Initialized(float chargepercentage, float direction)
    {
        charge = chargepercentage;
        rb.AddForce(transform.forward * direction * Mathf.Lerp(stats.minSpeed, stats.maxSpeed, charge), ForceMode.Impulse);
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        tr.gameObject.SetActive(true);
        foreach (var x in colliders)
            x.enabled = (true);
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
        tr.gameObject.SetActive(false);
        foreach (var x in colliders)
            x.enabled = (false);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponentInChildren<TrailRenderer>();
        colliders = GetComponentsInChildren<Collider>();
        AS = GetComponent<AudioSource>();
    }
}
