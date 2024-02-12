using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Runner_Pathfinding : MonoBehaviour
{
    private Base_Door currentDoor;

    private void Start(){
        Game_Manager.Instance.OnMoveRunners += Move;

        // Pick a random door to start
    }
    
    // Move to be called by event
    private void Move(){

    }
}
