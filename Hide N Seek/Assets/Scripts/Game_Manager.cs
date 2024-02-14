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
        
        Game_State_Manager.Instance.OnGameStateChange += HandleGameStateChange;
    }
    // Start is called before the first frame update
    void Start()
    {
        Game_State_Manager.Instance.SetGameState(GameState.RunnerTurn); //! Temporarily setting the game state to RunnerTurn
    }

    private void HandleGameStateChange(GameState gameState){
        switch(gameState){
            case GameState.RunnerTurn:
                HandleRunnerTurn();
                break;
        }
    }

    private void HandleRunnerTurn(){
        _amountOfRunnerMoves = 10;
        MoveRunners();
    }


    private void MoveRunners(){
        if(_amountOfRunnerMoves > 0){
            Debug.Log("Moving Runners");

            _amountOfRunnerMoves--;
            OnMoveRunners?.Invoke();
        }
        else Game_State_Manager.Instance.SetGameState(GameState.PlayerTurn); // Runners have picked their doors and now it's the players turn
    }

    public void RunnersFinishedRunning(){
        MoveRunners();
    }

    public void PlayerClickedDoor(Base_Door door){
        Game_State_Manager.Instance.SetGameState(GameState.RevealDoors);
        // Open all the doors (will require a door manager)
        // Compare the players door with the runners door
        if(_runner.CurrentDoor == door){
            Debug.Log("Player clicked the correct door");
            // Increment the players score
            // 
        } 
        else {
            Debug.Log("Player clicked the wrong door");
        }
    }
}
