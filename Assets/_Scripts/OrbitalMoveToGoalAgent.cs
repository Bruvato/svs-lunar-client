using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.SideChannels;
using Unity.VisualScripting;
using Unity.Sentis;
using System.Collections.Generic;

public class OrbitalMoveToGoalAgent : Agent
{
    [SerializeField] private GameObject target;
    [SerializeField] private ApplyGravity origin;
    [SerializeField] private float moveSpeedModifier;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0,15,0);
        target.GetComponent<InitialVelocity>().Launch();
        gameObject.GetComponent<InitialVelocity>().Launch();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);

        
        sensor.AddObservation(target.transform.localPosition);
        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveY = actions.ContinuousActions[2];

        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(moveX, moveY, moveZ)*moveSpeedModifier);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LoseZone")){
            AddReward(-5f);
            EndEpisode();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("WinZone")){
            SetReward(5f);
            EndEpisode();
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("LoseZone")){
            AddReward(-5f);
            EndEpisode();
        }
    }

}
