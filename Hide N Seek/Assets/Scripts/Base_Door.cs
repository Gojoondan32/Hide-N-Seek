using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Door : MonoBehaviour
{
    [SerializeField] private Transform _doorPosition;
    public Vector3 Position => _doorPosition.position;

}
