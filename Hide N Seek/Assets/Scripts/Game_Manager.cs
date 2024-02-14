using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class Game_Manager : MonoBehaviour
{
    #region Singleton Instance
    public static Game_Manager Instance { get; private set; }
    #endregion
    #region Events
    public delegate void MoveRunnersDelegate();
    public event MoveRunnersDelegate OnMoveRunners;
    #endregion
    #region Private Variables
    [SerializeField] private Runner_Pathfinding _runner; //! The runner should never be destroyed
    private int _amountOfRunnerMoves;
    #endregion
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        InitialiseGame(); //! Temporary (call this from an event or something else later on)
        MoveRunners(); //! Temporary (call this from an event or something else later on)
    }

    private void InitialiseGame(){
        _amountOfRunnerMoves = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0)) OnMoveRunners?.Invoke();
    }

    private void MoveRunners(){
        if(_amountOfRunnerMoves > 0){
            Debug.Log("Moving Runners");

            _amountOfRunnerMoves--;
            OnMoveRunners?.Invoke();
        }
    }

    public void RunnersFinishedRunning(){
        MoveRunners();
    }

    public void PlayerClickedDoor(Base_Door door){
        // Open all the doors (will require a door manager)
        // Compare the players door with the runners door
        if(_runner.CurrentDoor == door){
            Debug.Log("Player clicked the correct door");
        } else {
            Debug.Log("Player clicked the wrong door");
        }
    }
}
