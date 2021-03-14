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
        Vector3 targetRotation = Vector3.Angle(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.Rotate(targetRotation);
    }
}
