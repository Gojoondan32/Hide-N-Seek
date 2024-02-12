using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Manager 
{
    private GameObject[,] _doorMap;
    private GameObject _doorPrefab;

    public Door_Manager(int xSize, int ySize, GameObject doorPrefab){
        _doorMap = new GameObject[xSize, ySize];
        _doorPrefab = doorPrefab;

        CreateDoorMap();
    }

    private void CreateDoorMap(){
        // Create a map of doors
        for(int x = 0; x < _doorMap.GetLength(0); x++){
            for(int y = 0; y < _doorMap.GetLength(1); y++){
                _doorMap[x, y] = GameObject.Instantiate(_doorPrefab, new Vector3(x * 2, y * 2.5f, 2.5f), Quaternion.identity); ;
            }
        }
    }
    
    // Return any door that that the runner can move to based on the current door
    public Base_Door GetAvailableDoors(Base_Door currentDoor){
        return null;
    }
}
