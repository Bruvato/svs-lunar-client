using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;

public class NewMonoBehaviourScript : Agent
{

    [SerializeField] private Transform target;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0f, 0.3f, 0f);

        float rand1 = Random.Range(-3.5f, 3.5f);
        float rand2 = Random.Range(-1.7f, 1.7f);
        target.localPosition = new Vector3(rand1, 0.3f, rand2);

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
        float moveSpeed = 2;

        transform.localPosition += new Vector3(movex,0f,movez) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "pellet"){
            AddReward(10f);
            EndEpisode();
        }
        if(other.gameObject.tag == "walls"){
            AddReward(-5f);
            EndEpisode();
        }
    }
}
