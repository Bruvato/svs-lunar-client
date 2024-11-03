using UnityEngine;

public class JunkMovement : MonoBehaviour, IFall
{
    [SerializeField] private GameObject origin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Move(Vector3 direction, float speed){
        gameObject.GetComponent<Rigidbody>().AddForce(direction*speed);
        //Debug.Log(gameObject.GetComponent<Rigidbody>().linearVelocity.magnitude);
    }

    // Update is called once per frame
    public void Fall(Vector3 direction, float speed){
        Move(direction,speed);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        Vector3 direction = (origin.transform.localPosition-transform.localPosition).normalized;
        Move(direction, 1);
    }
}
