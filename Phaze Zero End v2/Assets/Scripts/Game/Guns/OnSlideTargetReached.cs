using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnSlideTargetReached : MonoBehaviour
{
    public float threshold = 0.02f;
    public Transform target;
    public UnityEvent OnReached;
    private bool wasReached = false;

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < threshold && !wasReached)
        {
            OnReached.Invoke();
            wasReached = true;
            Debug.Log("Slide Reached Target");
        }
        else if (distance >= threshold)
        {
            wasReached = false;
        }
    }
}
