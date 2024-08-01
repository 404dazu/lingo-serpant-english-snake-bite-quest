using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour
{
    // waypoint trigger
    bool entered = false;
    public bool Entered { get => entered; }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("quis");
        entered = true;
    }
}
