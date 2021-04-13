using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    /*
     * Original scripts credited to YouTube channel project-shasta
     * Edited for use case in project
     */
    public Doorway[] doorways;
    public MeshCollider meshCollider;

    public Bounds RoomBounds => meshCollider.bounds;
}
