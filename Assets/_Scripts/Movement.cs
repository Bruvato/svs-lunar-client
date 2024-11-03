using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class Movement : MonoBehaviour, IFall
{
    [SerializeField] private GameObject origin;
    private Vector3 acceleration;
    private float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Move(Vector3 direction, float speed){
        gameObject.GetComponent<Rigidbody>().AddForce(direction*speed);
    }

    // Update is called once per frame
    public void Fall(Vector3 direction, float speed){
        Move(direction,speed);
    }

    private void Update()
    {
        Vector3 direction = (origin.transform.localPosition-transform.localPosition).normalized;
        acceleration = direction*1;
        distance = (origin.transform.localPosition-transform.localPosition).magnitude;

        Move(direction, 1);
    }
    public Vector3 getAccelerationVector(){
        return acceleration;
    }

    public float GetDistance(){
        return distance;
    }
}
