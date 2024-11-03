using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;

public class testscript : Agent
{
    [SerializeField] private Transform target;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;

        float randomAngle = Random.Range(0f, 360f);
        float randomRadius = Random.Range(5, 10);


        transform.localPosition += Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward * randomRadius;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float movex = actions.ContinuousActions[0];
        float movez = actions.ContinuousActions[1];
        float movey = actions.ContinuousActions[2];

        float moveSpeed = 2;

        transform.localPosition += new Vector3(movex,movey,movez) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        continuousActions[2] = Input.GetAxisRaw("Jump");
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "pellet"){
            AddReward(10f);
            EndEpisode();
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "walls"){
            AddReward(-5f);
            EndEpisode();
        }
    }
    
}
