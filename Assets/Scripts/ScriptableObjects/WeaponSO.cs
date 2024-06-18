using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(order = 10, menuName = "RampartRampage/WeaponStats", fileName = "WeaponStats")]
public class WeaponSO : ScriptableObject
{
    [field:SerializeField] public float chargeUpTime {  get; private set; }
    [field: SerializeField , Range(0,1)] public float minChargePercent { get; private set; }
    [field: SerializeField] public float fireTime { get; private set; }
    [field: SerializeField] public bool isFullAuto { get; private set; }
    [field: SerializeField] public int numProjectiles { get; private set; }
    [field: SerializeField, Range(0, 90)] public float spreadAngle { get; private set; }
    [field: SerializeField] public float maxHoldTime { get; private set; }
}
