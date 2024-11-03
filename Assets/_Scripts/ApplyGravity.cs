using System;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class ApplyGravity : MonoBehaviour
{
    [SerializeField] private float gravityStrength;
    [SerializeField] private float launchVelocityModifier;

    [SerializeField] private List<GameObject> orbiters;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
    }
    public void Launch()
    {   
        foreach (GameObject target in orbiters){
            if (target.layer == LayerMask.NameToLayer("WinZone")){
            target.transform.localPosition = transform.localPosition + new Vector3(0,-14,0);
            }
            Vector3 direction = (transform.localPosition - target.transform.localPosition).normalized;

        
            
            Vector3 perpendicular= new Vector3(direction.y, -direction.x, direction.z);
            float degrees = UnityEngine.Random.Range(0.0f, 360.0f);
            perpendicular = Quaternion.AngleAxis(degrees,direction)*perpendicular;
            target.GetComponent<Rigidbody>().linearVelocity= perpendicular* (float)Math.Sqrt(gravityStrength*launchVelocityModifier*(transform.localPosition - target.transform.localPosition).magnitude);

        }
    }
 
    // Update is called once per frame
    void Update()
    {
        foreach (GameObject target in orbiters){
            Vector3 direction = (transform.localPosition - target.transform.localPosition).normalized;
            IFall targ = target.GetComponent<IFall>();
            targ.Fall(direction, gravityStrength);

        }
        
    }
    }

