using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoal : Agent
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private Transform targetTransform;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;

        targetTransform.localPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        //float moveY = actions.ContinuousActions[1];
        float moveZ = actions.ContinuousActions[1];

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        AddReward(-0.01f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        //continuousActions[1] = Input.GetAxisRaw("Jump");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            AddReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Reward>(out Reward reward))
        {
            AddReward(+1f);
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            AddReward(-1f);
            EndEpisode();
        }
    }
}
