using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;

public class Runner_Pathfinding : MonoBehaviour
{
    [SerializeField] private Base_Door _currentDoor;
    public Base_Door CurrentDoor {get {return _currentDoor;} private set {_currentDoor = value;}}
    private Base_Door _targetDoor;
    private bool _isMoving;
    private float _speed = 5f;
    [SerializeField]private bool _isMimic;
    private void Awake(){
        Game_Manager.Instance.OnMoveRunners += Move;

        _isMoving = false;
    }

    public void InitRunnerState(Base_Door door){
        // This should only be called on a runner at the start of the game
        _isMimic = false;
        SetCurrentDoor(door);
        transform.position = door.GetDoorTransform().position;
    }

    public void InitMimicState(Base_Door door){
        // This should only be called on a mimic runner
        _isMimic = true;
        SetCurrentDoor(door);
        transform.position = door.GetDoorTransform().position;
        Game_State_Manager.Instance.OnGameStateChange += DestroyMimic;
    }
    
    // Move to be called by event
    private void Move(){
        Random.InitState(System.DateTime.Now.Millisecond);
        int random = Random.Range(0, 2);
        if(random == 0){
            // Move to the left door
            _targetDoor = CurrentDoor.LeftDoor;
        } else {
            // Move to the right door
            _targetDoor = CurrentDoor.RightDoor;
        }
        _isMoving = true;
    }

    private void Update(){
        if(!_isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, _targetDoor.Position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.forward, _targetDoor.Position - transform.position, Vector3.up), Vector3.up);
        if(Vector3.Distance(transform.position, _targetDoor.Position) < 0.01f){
            _isMoving = false;
            _targetDoor.Arrived(this);
            if(!_isMimic) Game_Manager.Instance.RunnersFinishedRunning(); // Only call this if the runner is not a mimic
        }
        
    }

    public void SetCurrentDoor(Base_Door door) => CurrentDoor = door;

    private void DestroyMimic(GameState gameState){
        if(!_isMimic) return;

        if(gameState == GameState.PlayerTurn) Destroy(gameObject);
            
        
    }

    private void OnDestroy() {
        Game_Manager.Instance.OnMoveRunners -= Move;
        Game_State_Manager.Instance.OnGameStateChange -= DestroyMimic;
    }
}
