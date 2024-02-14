using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;

public class Runner_Pathfinding : MonoBehaviour
{
    [SerializeField] private Base_Door _currentDoor;
    private Base_Door _targetDoor;
    private bool _isMoving = false;
    private float _speed = 5f;
    private void Start(){
        Game_Manager.Instance.OnMoveRunners += Move;

        // Pick a random door to start
    }
    
    // Move to be called by event
    private void Move(){
        Random.InitState(System.DateTime.Now.Millisecond);
        int random = Random.Range(0, 2);
        if(random == 0){
            // Move to the left door
            //_targetDoor = _currentDoor.LeftDoor;
        } else {
            // Move to the right door
            
        }
        _targetDoor = _currentDoor.RightDoor;
        _isMoving = true;
    }

    private void Update(){
        if(!_isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, _targetDoor.Position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.forward, _targetDoor.Position - transform.position, Vector3.up), Vector3.up);
        if(Vector3.Distance(transform.position, _targetDoor.Position) < 0.1f){
            _isMoving = false;
            _targetDoor.Arrived(this);
            
        }
        
    }

    public void SetCurrentDoor(Base_Door door){
        _currentDoor = door;
        Debug.Log(_currentDoor.gameObject.name);
        
    } 
}
