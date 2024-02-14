using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Game_State_Manager.Instance.CurrentGameState != GameState.PlayerTurn) return;

        if(Input.GetMouseButtonDown(0)) PickDoor();
    }

    private void PickDoor(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue)){
            if (hit.collider.TryGetComponent(out Base_Door door)){
                Debug.Log("Player clicked a door");
                Game_Manager.Instance.PlayerClickedDoor(door);
            }
        }
    }
}
