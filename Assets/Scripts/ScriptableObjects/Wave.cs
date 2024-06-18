using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(order = 10, menuName = "RampartRampage/Wave", fileName = "Round")]
public class Wave : ScriptableObject
{
    [field:SerializeField] public SpawnInfo[] spawnEnemies { get; private set; }
    [field: SerializeField] public float dayDuration { get; private set; }

    [Serializable]
   public struct SpawnInfo
    {
        public Enemy Enemy;
        public int Amount;
    }
}
