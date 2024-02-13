using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Door : Base_Door
{
    [SerializeField] private Base_Door _linkedDoor;

    private void Start()
    {
        Debug.Log(_doorPosition);
    }

    public override void Arrived(Runner_Pathfinding runnerTransform = null)
    {
        runnerTransform.transform.position = _linkedDoor.Position;
        SetCurrentDoor(runnerTransform);
    }
}
