using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.SideChannels;
using Unity.VisualScripting;

public class OrbitalMoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[0];


        float moveSpeedModifier =1f;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(moveX*moveSpeedModifier, moveY*moveSpeedModifier, moveZ*moveSpeedModifier));
    }
}
