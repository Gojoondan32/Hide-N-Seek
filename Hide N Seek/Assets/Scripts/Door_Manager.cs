using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class Door_Manager : MonoBehaviour
{
    [SerializeField] private List<Base_Door> _doors;

    private void Awake() {
        Game_State_Manager.Instance.OnGameStateChange += HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState gameState){
        if(gameState != GameState.RevealDoors) return;

        StartCoroutine(RotateDoors());
    }

    private IEnumerator RotateDoors(){
        // Play the door open animation
        foreach (Base_Door door in _doors){
            door.OpenDoors();
        }

        yield return new WaitForSeconds(1f);
        Game_Manager.Instance.CheckPlayersDoor();
        yield return new WaitForSeconds(1f);

        // Play the door close animation
        foreach (Base_Door door in _doors){
            door.CloseDoors();
        }

        if(Game_State_Manager.Instance.CurrentGameState != GameState.GameOver){
            Game_State_Manager.Instance.SetGameState(GameState.RunnerTurn);
        }
    }

    public Base_Door GetRandomDoor(){
        Random.InitState(System.DateTime.Now.Millisecond); // Seed the random number generator
        return _doors[Random.Range(0, _doors.Count)];
    }
}
