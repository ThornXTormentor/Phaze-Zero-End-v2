using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject elevator;

    private enum ElevatorStates
    {
        goingUp,
        goingDown,
        stopped
    };

    private ElevatorStates states;

    public Transform topPos;
    public Transform bottomPos;
    private Transform currentPos;
    public float smoothing = 1.75f;

    private Vector3 newPos;
    private bool hasPlayer;

    // Start is called before the first frame update
    void Start()
    {
        elevator.SetActive(false);
        states = ElevatorStates.stopped;
        currentPos = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayer && currentPos == bottomPos)
        {
            states = ElevatorStates.goingUp;
        }

        if (hasPlayer && currentPos == topPos)
        {
            states = ElevatorStates.goingDown;
        }
        
        FiniteStateChange();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            elevator.SetActive(true);
            other.transform.parent = gameObject.transform;
            hasPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            elevator.SetActive(false);
            other.transform.parent = null;
            hasPlayer = false;
        }
    }

    public void FiniteStateChange()
    {
        if (states == ElevatorStates.goingDown)
        {
            newPos = bottomPos.position;
            transform.position = Vector3.Lerp(transform.position, newPos, smoothing * Time.deltaTime);
        }
        
        if (states == ElevatorStates.goingUp)
        {
            newPos = topPos.position;
            transform.position = Vector3.Lerp(transform.position, newPos, smoothing * Time.deltaTime);
        }
        
        if (states == ElevatorStates.goingDown)
        {
            newPos = bottomPos.position;
            transform.position = Vector3.Lerp(transform.position, newPos, smoothing * Time.deltaTime);
        }
        
    }
}
