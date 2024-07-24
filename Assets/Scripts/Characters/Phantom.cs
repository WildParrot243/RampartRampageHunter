using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class Phantom : Enemy
{
    private int path;
    private int index;
    private Vector3 target;
    private Vector3 direction;
    private const float stoppingDist = 1;
    
    private void Awake()
    {
        path = WaypointManager.ChooseRandomPath();
        transform.position = WaypointManager.GetNextLocation(path, ref index);
        NewPoint();
    }

    private void FixedUpdate()
    {
        //Translate between points
        float singleStep = (Time.fixedDeltaTime * stats.Speed);
        transform.position += direction * singleStep;// some speed value;
        transform.rotation =  Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, singleStep, 1f));
        //If we've reached the point, move towards the next point
        if (Vector3.Distance(target, transform.position) < stoppingDist)
        {
            NewPoint();
        }
    }

    private void NewPoint()
    {
        target = WaypointManager.GetNextLocation(path, ref index);
        direction = (target - transform.position).normalized;
    }
}
