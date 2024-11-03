using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.SideChannels;
using Unity.VisualScripting;
using Unity.Sentis;
using System.Collections.Generic;
using System;
using Unity.Mathematics.Geometry;

public class OrbitalMoveToGoalAgent : Agent
{
    [SerializeField] private GameObject target;
    [SerializeField] private ApplyGravity origin;
    [SerializeField] private float moveSpeedModifier;
     private float dist, lowestdist;


    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0,15,0);
        target.GetComponent<InitialVelocity>().Launch();
        gameObject.GetComponent<InitialVelocity>().Launch();
        dist = (transform.localPosition-target.transform.localPosition).magnitude;
        lowestdist = dist;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.transform.localPosition);
        sensor.AddObservation(gameObject.GetComponent<Movement>().getAccelerationVector());
        sensor.AddObservation(lowestdist);
        sensor.AddObservation(dist);


        
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
            AddReward(-1000f);

            EndEpisode();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("WinZone")){
            AddReward(100000f);
            EndEpisode();
        }
        AddReward(UnityEngine.Mathf.Pow(MathF.E,-0.1f*lowestdist+2.5f)-2);

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("LoseZone")){
           AddReward(-1000f);
           EndEpisode();
        }
        AddReward(UnityEngine.Mathf.Pow(MathF.E,-0.1f*lowestdist+2.5f)-2);
    }
    void Update(){
        dist = (transform.localPosition-target.transform.localPosition).magnitude;
        if(dist<lowestdist){
            lowestdist = dist;
        }
    }

}
