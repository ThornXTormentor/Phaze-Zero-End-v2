using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Transform playerTransform;

    public void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        //Focus direction on player
        Vector3 direction = playerTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    public void LookAtPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        var playerPosition = playerTransform.position;
        Vector3 targetPosition = playerPosition - transform.position;
        float rotateStep = 1 * Time.deltaTime;
        float targetRotation = Vector3.Angle( transform.position,targetPosition);
        Vector3 targetAxis = Vector3.Cross(transform.forward, targetPosition);
        transform.RotateAround(transform.position,targetAxis,rotateStep * targetRotation);
        Debug.Log("looking at player");
    }
}
