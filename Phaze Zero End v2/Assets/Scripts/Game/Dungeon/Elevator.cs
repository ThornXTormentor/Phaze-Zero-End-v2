using System;
using UnityEngine;

namespace Game.Dungeon
{
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
            //elevator.SetActive(true);
            states = ElevatorStates.stopped;
            currentPos = bottomPos;
            this.transform.position = bottomPos.position;
        }

        private void Update()
        {
            switch (states)
            {
                case ElevatorStates.goingUp:
                    newPos = topPos.position;
                    this.transform.position = Vector3.Lerp(transform.position, newPos, smoothing * Time.deltaTime);
                    currentPos = topPos;
                    break;
                case ElevatorStates.goingDown:
                    newPos = bottomPos.position;
                    this.transform.position = Vector3.Lerp(transform.position, newPos, smoothing * Time.deltaTime);
                    currentPos = bottomPos;
                    break;
                case ElevatorStates.stopped:
                    break;
                default:
                    break;
                    
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && currentPos == bottomPos && states == ElevatorStates.stopped)
            {
                Debug.Log("player found");
                states = ElevatorStates.goingUp;
            }
            else if (other.CompareTag("Player") && currentPos == topPos && states == ElevatorStates.stopped)
            {
                Debug.Log("player found");
                states = ElevatorStates.goingDown;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && (currentPos == topPos || currentPos == bottomPos))
            {
                Debug.Log("Player left");
                states = ElevatorStates.stopped;
            }
        }
        
    }
}
