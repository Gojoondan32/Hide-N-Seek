using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Door : MonoBehaviour
{
    [SerializeField] private Base_Door _leftDoor, _rightDoor;
    [HideInInspector] public Base_Door LeftDoor => _leftDoor;
    [HideInInspector] public Base_Door RightDoor => _rightDoor;
    [SerializeField] protected Transform _doorPosition;
    public Vector3 Position {
        get {
            return _doorPosition.position;
        }
    }

    public virtual void Arrived(Runner_Pathfinding runnerTransform = null){
        Debug.Log("Arrived at door");
        SetCurrentDoor(runnerTransform);
    }

    protected void SetCurrentDoor(Runner_Pathfinding runnerTransform){
        runnerTransform.SetCurrentDoor(this);
    }

}
