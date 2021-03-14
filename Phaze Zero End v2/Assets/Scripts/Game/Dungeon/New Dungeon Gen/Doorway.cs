using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    /*
     * Original scripts credited to YouTube channel project-shasta
     * Edited for use case in project
     */
    void OnDrawGizmos ()
    {
        Ray ray = new Ray (transform.position, transform.rotation * Vector3.forward);

        Gizmos.color = Color.red;
        Gizmos.DrawRay (ray);
    }
}
