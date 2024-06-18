using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour
    
{

    [SerializeField] protected WeaponSO stats;
    [SerializeField] protected Transform theFirePoint;

    private bool isFiring;
    private float currentFireTime;
    private bool isOnCooldown;
    private Animator animator;
    private Coroutine fireLoop;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat(StaticUtilites.animReloadSpeed, 1 / stats.fireTime);
        animator.SetFloat(StaticUtilites.animFireSpeed, 5 / stats.chargeUpTime);

    }

    protected abstract void Activate(float chargepercent, Quaternion rotation);
    

    protected void Reload()
    {

    }

    protected virtual bool CanActivate(float chargepercent)
    {
        return chargepercent >= stats.minChargePercent && ! isOnCooldown;
    }
    public void TryActivate()
    {
        animator.SetBool(StaticUtilites.animIsReady, true);
        fireLoop = StartCoroutine(ActivationHandler());
        isFiring = true;
    }

    public void StopActivate()
    {
        if (fireLoop != null) StopCoroutine(fireLoop);
        isFiring = false;
        float p = currentFireTime / stats.chargeUpTime;
        if (CanActivate(p)) PreActivate(p);
        animator.SetBool(StaticUtilites.animIsReady, false);
        currentFireTime = 0;
    }

    private void PreActivate(float chargepercent)
    {
        currentFireTime = 0;
        isOnCooldown = true;
        StartCoroutine(HandleCooldown()); 
        Quaternion rotation = transform.rotation;
        Activate(chargepercent, rotation);
        animator.SetTrigger(StaticUtilites.animReload);
        for (int i = 1; i < stats.numProjectiles;  i += 1)
        {
            // Randomize the rotation for subsequent projectiles
            float randomHorizontalAngle = Random.Range(-stats.spreadAngle, stats.spreadAngle);
            float randomVerticalAngle = Random.Range(-stats.spreadAngle, stats.spreadAngle);

            // Apply the random rotation
            Quaternion randomRotation = rotation * Quaternion.Euler(randomVerticalAngle, randomHorizontalAngle, 0);
            Activate(chargepercent, randomRotation);
        }
    

    }
    private IEnumerator ActivationHandler()
    {

        do
        {
            yield return new WaitWhile(() => isOnCooldown);

            while (currentFireTime < stats.chargeUpTime)
            {
                currentFireTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(stats.maxHoldTime - currentFireTime);
            if (CanActivate(1)) PreActivate(1);
        } while (stats.isFullAuto);
        isFiring = false;
        animator.SetBool(StaticUtilites.animIsReady, false);
    }
    
    private IEnumerator HandleCooldown()
    {

        isOnCooldown = true;
        yield return new WaitForSeconds(stats.fireTime);
        isOnCooldown = false;
        
    }




}
