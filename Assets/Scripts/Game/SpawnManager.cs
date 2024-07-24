using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Wave[] rounds;
    private int currentRound;
    private Vector3[] spawn;
    private Transform parent;
    private float currentTime;
    private float endTime;
    private int numEnemies;
    private int[] enemyCounts;
    public static bool isRoundRunning => Instance.enabled;
    public static SpawnManager Instance { get; private set; }
    public static int numWaves => Instance.rounds.Length;
 
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;

        }
        Instance = this;

        spawn = new Vector3[transform.childCount];
        for (int i = 0; i < spawn.Length; i+= 1) 
        {

            spawn[i] = transform.GetChild(i).position; 
        }

        ClearEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > endTime)
        {
            SpawnEnemy();
            currentTime = 0;
        }
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, numEnemies);
        for (int i = 0; i < enemyCounts.Length; i += 1) 
        {
            int val = enemyCounts[i];
            if (index < val)
            {
                enemyCounts[i] -= 1;
                numEnemies -= 1;
                index = i;
                break;
            }

            index -= val;

        }

        currentTime = 0;
        if (numEnemies == 0) 
        enabled = false;
        Instantiate(rounds[currentRound].spawnEnemies[index].Enemy, spawn[Random.Range(0, spawn.Length)], Quaternion.identity, parent);

    }
    public void ClearEnemies()
    {
        if (parent != null)
        {
          Destroy(parent.gameObject);
        }

        parent = new GameObject("SpawnEnemies").transform;
        enabled = false;
    }

    public int StartRound(int round )
    {
        enabled = true;
        currentRound = round;
        enemyCounts = new int[rounds[currentRound].spawnEnemies.Length];
        numEnemies = 0;
        for (int i = 0; i < rounds[currentRound].spawnEnemies.Length; i += 1)
        {
            numEnemies += rounds[currentRound].spawnEnemies[i].Amount;
            enemyCounts[i] = rounds[currentRound].spawnEnemies[i].Amount;
        }

        endTime = rounds[currentRound].dayDuration / numEnemies;
        return numEnemies;
    }
}
