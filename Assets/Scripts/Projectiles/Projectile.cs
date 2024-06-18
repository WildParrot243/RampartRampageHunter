using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float minDamage;
    [SerializeField] protected float maxDamage;
    float charge;
    Rigidbody rb;
    TrailRenderer tr;
    Collider[] colliders;
    

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision);
    }

    protected virtual void OnHit(Collision collision)
    {
        print(collision.gameObject.name + ", " + Mathf.Lerp(minDamage, maxDamage, charge));
    }

    public void Initialized(float chargepercentage, float direction)
    {
        charge = chargepercentage;
        rb.AddForce(transform.forward * direction, ForceMode.Impulse);
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
    }
}
