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

        // Play the door open animation
        StartCoroutine(OpenDoors());
    }

    private IEnumerator OpenDoors(){
        float time = 0;
        while(time < 2){
            time += Time.deltaTime;
            foreach (var door in _doors){
                door.transform.rotation = Quaternion.Slerp(door.transform.rotation, Quaternion.Euler(0, 90, 0), time);
            }
            yield return null;
        }
        Debug.Log("Doors are open");
        //Game_State_Manager.Instance.SetGameState(GameState.PlayerTurn);
    }
}
