using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Stats stats;
    public static GameManager Instance { get; private set;}

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;

        }
        Instance = this;
        //GameSaver.SaveData.castleHealth -= 10;
        //print(GameSaver.SaveData.castleHealth);
        // GameSaver.Save();

        stats = GetComponent<Stats>();
       

    }

    void Start()
    {
      
        print(GameSaver.SaveData.waveNumber);
      SpawnManager.Instance.StartRound(GameSaver.SaveData.waveNumber);
        stats.OnNewRound();
    }


    public void OnRoundEnd()
    {
        
        SpawnManager.Instance.ClearEnemies();
        GameSaver.SaveData.waveNumber += 1;

        if (GameSaver.SaveData.waveNumber >= SpawnManager.numWaves)
        {
            GameSaver.SaveData.gameStats += 1;
            GameSaver.SaveData.waveNumber = 0;

            if (GameSaver.SaveData.gameStats >= stats.numNewGames)
            {
                OnGameEnd();
                GameSaver.SaveData.gameStats = 0;
            }
        }
        GameSaver.Save();
        stats.OnNewRound();
    }

    private void OnGameEnd()
    {

    }

}

