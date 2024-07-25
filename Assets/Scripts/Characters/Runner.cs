using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Runner : Enemy
{
    protected NavMeshAgent movement;

    private void Start()
    {
        movement = GetComponent<NavMeshAgent>();
        movement.SetDestination(Castle.pos);
        movement.speed = stats.Speed;
    }
}
