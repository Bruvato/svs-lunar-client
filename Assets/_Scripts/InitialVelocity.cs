using System;
using UnityEngine;
using UnityEngine.WSA;

public class InitialVelocity : MonoBehaviour
{
    [SerializeField] private GameObject origin;
    [SerializeField] private float gravityStrength;
    [SerializeField] private float launchVelocityModifier;
    [SerializeField] private Vector3 startingOffset;

    public void Launch()
    {

        transform.localPosition = origin.transform.localPosition + startingOffset;

        Vector3 direction = (origin.transform.localPosition - transform.localPosition ).normalized;


        Vector3 perpendicular = new Vector3(direction.y, -direction.x, direction.z);
        float degrees = UnityEngine.Random.Range(0.0f, 360.0f);
        perpendicular = Quaternion.AngleAxis(degrees, direction) * perpendicular;
        gameObject.GetComponent<Rigidbody>().linearVelocity = perpendicular * (float)Math.Sqrt(gravityStrength * launchVelocityModifier * (transform.localPosition - origin.transform.localPosition).magnitude);
    }
 
}
