using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movebox : MonoBehaviour
{
    public Transform t;
    public WheelCollider w1;
    public WheelCollider w2;
    public Transform weapen1;
    public Transform weapen2;
    private void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.W) && w1.motorTorque>0) 
        {
            w1.motorTorque = -100;
            w2.motorTorque = -100;
        }
        if (Input.GetKey(KeyCode.A))
        {
            w1.steerAngle = -20;
            w2.steerAngle = -20;
            //transform.position = transform.position + Vector3.forward;
            //rd.AddForce(-1000*Time.deltaTime,0,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            w1.steerAngle = 20;
            w2.steerAngle = 20;
            //rd.AddForce(1000*Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            w1.motorTorque = 1000;
            w2.motorTorque = 1000;
            w1.brakeTorque = 0;
            w2.brakeTorque = 0;
            //rd.AddForce(0, 0, 1000 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            w1.motorTorque = -1000;
            w2.motorTorque = -1000;
            w1.brakeTorque = 0;
            w2.brakeTorque = 0;
            //rd.AddForce(0, 0, -1000 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            w1.brakeTorque = 5000;
            w2.brakeTorque = 5000;
            t.GetComponent<ParticleSystem>().enableEmission = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            t.GetComponent<ParticleSystem>().enableEmission = false;
        }
    }
}
