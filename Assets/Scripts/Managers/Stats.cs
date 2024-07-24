using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Serializable]
   public struct GameStats
    {
      [field: SerializeField]  public float multilperHealth { get; private set; }
      [field: SerializeField]  public float multilperSpeed { get; private set; }
      [field: SerializeField]  public float multilperDamage { get; private set; }
      [field: SerializeField] public float multilperValue { get; private set; }

    }

    public int numNewGames => stats.Length; 
    [SerializeField] private GameStats[] stats;
    public static GameStats gameStats { get; private set; }

    public void OnNewRound()
    {
        gameStats = stats[GameSaver.SaveData.gameStats];
        
    }
}

