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
    [SerializeField] private UI_Manager _uiManager;
    [SerializeField] private Runner_Pathfinding _runner; //! The runner should never be destroyed
    [SerializeField] private Door_Manager _doorManager;
    private int _amountOfRunnerMoves;
    private Base_Door _doorPlayerPicked;
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
        Game_State_Manager.Instance.SetGameState(GameState.MainMenu);
    }

    private void HandleGameStateChange(GameState gameState){
        switch(gameState){
            case GameState.MainMenu:
                _runner.InitRunnerState(_doorManager.GetRandomDoor());
                break;
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
        _doorPlayerPicked = door;
        Game_State_Manager.Instance.SetGameState(GameState.RevealDoors);
    }

    public void CheckPlayersDoor(){
        // Compare the players door with the runners door
        if (_runner.CurrentDoor == _doorPlayerPicked){
            Debug.Log("Player clicked the correct door");
            // Increment the players score
            _uiManager.AddToPlayerScore(1);
        }
        else{
            Debug.Log("Player clicked the wrong door");
            // Decrement the players lives
            _uiManager.RemoveFromPlayerLives(1);
        }
    }

}
