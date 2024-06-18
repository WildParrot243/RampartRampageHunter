using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticUtilites 
{
    public static readonly int animReload = Animator.StringToHash("Reload");
    public static readonly int animIsReady = Animator.StringToHash("IsReady");
    public static readonly int animReloadSpeed = Animator.StringToHash("ReloadSpeed");
    public static readonly int animFireSpeed = Animator.StringToHash("FireSpeed");


    public static readonly int EnemyLayerID = 1 << LayerMask.NameToLayer("Enemy");
    public static readonly int ProjectileLayerID = 1 << LayerMask.NameToLayer("Projectile");
    public static readonly int Default = 1 << LayerMask.NameToLayer("Default");

    public static readonly int BlockingLayers = Default | EnemyLayerID;

}

