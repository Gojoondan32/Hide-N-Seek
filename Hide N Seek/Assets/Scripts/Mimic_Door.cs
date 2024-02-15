using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic_Door : Base_Door
{
    [SerializeField] private Runner_Pathfinding _mimicRunner;
    public override void Arrived(Runner_Pathfinding runnerTransform){
        base.Arrived(runnerTransform);
        Runner_Pathfinding mimicRunner = Instantiate(_mimicRunner, runnerTransform.transform.position, Quaternion.identity);
        mimicRunner.InitMimicState(this);
    }


}
