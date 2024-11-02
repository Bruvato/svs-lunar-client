using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class ApplyGravity : MonoBehaviour
{
    [SerializeField] private float gravityStrength;
    GameObject[] targets;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        targets = GameObject.FindGameObjectsWithTag("GravityTarget");
        foreach (GameObject target in targets){
            Vector3 direction = (transform.position - target.transform.position).normalized;

            IFall targ = target.GetComponent<IFall>();
            targ.Fall(new Vector3(direction.y, -direction.x, direction.z), gravityStrength);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject target in targets){
            Vector3 direction = (transform.position - target.transform.position).normalized;

            IFall targ = target.GetComponent<IFall>();
            targ.Fall(direction, gravityStrength);
        }
        
    }
}
