using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Door : MonoBehaviour
{
    [SerializeField] private Base_Door _leftDoor, _rightDoor;
    [HideInInspector] public Base_Door LeftDoor => _leftDoor;
    [HideInInspector] public Base_Door RightDoor => _rightDoor;
    [SerializeField] protected Transform _doorPosition;
    [SerializeField] private Animator _animator;
    public Vector3 Position {
        get {
            return _doorPosition.position;
        }
    }

    public virtual void Arrived(Runner_Pathfinding runnerTransform){
        SetCurrentDoor(runnerTransform);
    }

    protected void SetCurrentDoor(Runner_Pathfinding runnerTransform, Base_Door door = null){
        runnerTransform.SetCurrentDoor(door ?? this);
    }

    public Transform GetDoorTransform() => _doorPosition;

    public virtual void OpenDoors(){
        // Play the door open animation
        _animator.SetBool("Close", false);
        _animator.SetBool("Open", true);
    }

    public virtual void CloseDoors(){
        // Play the door close animation
        _animator.SetBool("Open", false);
        _animator.SetBool("Close", true);
    }

}
