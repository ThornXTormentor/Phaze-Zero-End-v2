using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Transform playerTransform;
    public void LookAtPlayer()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        //float targetRotation = Vector3.Angle(playerTransform.position, playerTransform.position);
        //transform.Rotate(targetRotation);
    }
}
