using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private Projectile projectile;
    protected override void Activate(float chargepercent, Quaternion rotation)
    {
        float fores = Mathf.Lerp(minForce, maxForce, chargepercent);
        Instantiate(projectile, theFirePoint.position, rotation).Initialized(chargepercent, fores);
        

    }

    private void Start()
    {

        Instantiate(projectile, theFirePoint).enabled = false;
        
    }
}
