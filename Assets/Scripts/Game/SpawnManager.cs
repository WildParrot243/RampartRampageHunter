using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Wave[] rounds;
    private Vector3[] spawn;
    private Transform parent;
    private float currentTime;
    private float endTime;
    private int numEnemies;
    // Start is called before the first frame update
    void Start()
    {
        spawn = new Vector3[transform.childCount];
        for (int i = 0; i < spawn.Length; i+= 1) 
        {

            spawn[i] = transform.GetChild(i).position; 
        }
        
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
    }
    private void ClearEnemies()
    {
        if (parent != null)
        {
          Destroy(parent.gameObject);
        }

        parent = new GameObject("SpawnEnemies").transform;
    }

    private void StartRound()
    {

    }
}
