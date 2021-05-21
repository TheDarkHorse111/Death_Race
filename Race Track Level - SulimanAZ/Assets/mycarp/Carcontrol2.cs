using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carcontrol2 : MonoBehaviour
{
    public WheelCollider fwl;
    public WheelCollider fwr;
    public WheelCollider bwl;
    public WheelCollider bwr;
    public float speedwheel = 600.0f;
    public float steeringspeed = 45.0f;
    public float brakepower = 700f;
    public Transform fwlt;
    public Transform fwrt;
    public Transform bwlt;
    public Transform bwrt;
    public Transform []smokes;

    public void Start()
    {
        for (int i = 0; i < smokes.Length; i++)
        {
            smokes[i].GetComponent<ParticleSystem>().enableEmission = false;
        }
    }
    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            DriveFront();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            DriveBack();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            ResetMotor();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            ResetMotor();
        }
        //if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    fwl.motorTorque = 0;
        //    fwr.motorTorque = 0;
        //    bwl.motorTorque = 0;
        //    bwr.motorTorque = 0;

        //    fwl.brakeTorque = 5000;
        //    fwr.brakeTorque = 5000;
        //    bwl.brakeTorque = 5000;
        //    bwr.brakeTorque = 5000;
        //}
        if (Input.GetKey(KeyCode.RightArrow))
        {
            DriveRight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            BackSteering();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            DriveLeft();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            BackSteering();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            StopWheel();
            for (int i = 0; i < smokes.Length; i++)
            {
                smokes[i].GetComponent<ParticleSystem>().enableEmission = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            for (int i = 0; i < smokes.Length; i++)
            {
                smokes[i].GetComponent<ParticleSystem>().enableEmission = false;
            }
            Drifting();
            
        }
        WheelLocation(fwl,fwlt);
        WheelLocation(fwr, fwrt);
        WheelLocation(bwl, bwlt);
        WheelLocation(bwr, bwrt);
    }
    private void ResetMotor()
    {
            fwl.brakeTorque = brakepower;
            fwr.brakeTorque = brakepower;
            bwl.brakeTorque = brakepower;
            bwr.brakeTorque = brakepower;
            
    }
    private void BackSteering() 
    {
        fwl.steerAngle = 0;
        fwr.steerAngle = 0;
    }
    private void DriveFront() 
    {
        fwl.motorTorque = speedwheel;
        fwr.motorTorque = speedwheel;
        bwl.motorTorque = speedwheel;
        bwr.motorTorque = speedwheel;

        fwl.brakeTorque = 0;
        fwr.brakeTorque = 0;
        bwl.brakeTorque = 0;
        bwr.brakeTorque = 0;
    }
    private void DriveBack() 
    {
        fwl.motorTorque = -speedwheel;
        fwr.motorTorque = -speedwheel;
        bwl.motorTorque = -speedwheel;
        bwr.motorTorque = -speedwheel;

        fwl.brakeTorque = 0;
        fwr.brakeTorque = 0;
        bwl.brakeTorque = 0;
        bwr.brakeTorque = 0;
    }
    private void DriveLeft() 
    {
        fwl.steerAngle = -steeringspeed;
        fwr.steerAngle = -steeringspeed;
    }
    private void DriveRight() 
    {
        fwl.steerAngle = steeringspeed;
        fwr.steerAngle = steeringspeed;
    }
    private void StopWheel()
    {
        fwl.brakeTorque = brakepower;
        fwr.brakeTorque = brakepower;
        bwl.brakeTorque = brakepower;
        bwr.brakeTorque = brakepower;
    }
    private void Drifting()
    {
        bwl.brakeTorque = 7000;
        bwr.brakeTorque = 7000;
    }
    private void WheelLocation(WheelCollider wheelcar, Transform wheeltransform)
    {
        Vector3 positionwheel;
        Quaternion rotationwheel;

        wheelcar.GetWorldPose(out positionwheel, out rotationwheel);
        wheeltransform.position = positionwheel;
        wheeltransform.rotation = rotationwheel;
    }
}
