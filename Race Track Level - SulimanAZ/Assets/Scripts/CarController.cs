using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float Transformation;
    float rotation;
    float Brakes;
    public WheelCollider[] frontWheelColliders = new WheelCollider[2];
    public WheelCollider[] backWheelColliders = new WheelCollider[2];
    public Transform[] frontWheels = new Transform[2];
    public Transform[] backWheels = new Transform[2];
    private float SteeringAngle = 6f;
    public float maxSteeringAngle = 6f;
    private float maxSpeed = 250f;
    
    public float motorTorgue = 500f;
    public float maxbrakeTorgue = 2200f;
    public Transform COM;
    public float currentSpeed;
    private Rigidbody rb;

    [Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
    public float criticalSpeed = 5f;
    [Tooltip("Simulation sub-steps when the speed is above critical.")]
    public int stepsBelow = 5;
    [Tooltip("Simulation sub-steps when the speed is below critical.")]
    public int stepsAbove = 1;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = COM.localPosition;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        GetInput();
        
        Accelerate();
        Brake();
        

    }
    private void Update()
    {
        backWheelColliders[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);
        Steer();
        UpdateWheelPoses();
    }



    public void GetInput()
    {
        Transformation = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");
        Brakes = Input.GetAxis("Jump");

    }

    void Steer()
    {
        SteeringAngle = maxSteeringAngle;
        if (rotation > 0)
        {
            frontWheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f/ (SteeringAngle + (1.5f/2))) * rotation;
            frontWheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (SteeringAngle - (1.5f / 2))) * rotation;
        }
        else if (rotation < 0)
        {
            frontWheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (SteeringAngle - (1.5f / 2))) * rotation;
            frontWheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (SteeringAngle + (1.5f / 2))) * rotation;
        }
        else 
        {
            frontWheelColliders[0].steerAngle = 0;
            frontWheelColliders[1].steerAngle = 0;
        }
    }
    private void drift() 
    {
        WheelHit hit;
        foreach (WheelCollider w in backWheelColliders)
        {
            if (w.GetGroundHit(out hit)) 
            {

                SteeringAngle = 4 + (-Mathf.Abs(hit.sidewaysSlip) * 2) * rb.velocity.magnitude / 10;
                 

            }
               
        }
    }

    void Accelerate()
    {
        foreach (WheelCollider w in backWheelColliders)
        {
            if (currentSpeed < maxSpeed)
            {
                w.motorTorque = Transformation * motorTorgue;
          
            }
        }

        currentSpeed = 2 * 22 / 7 * backWheelColliders[0].radius * backWheelColliders[0].rpm * 60 / 1000;
    }

    void Brake() 
    {
        foreach (WheelCollider w in backWheelColliders) 
        {
            w.brakeTorque = maxbrakeTorgue * Brakes;
        }
        foreach (WheelCollider w in frontWheelColliders)
        {
            w.brakeTorque = maxbrakeTorgue * Brakes;
        }
    }

    void UpdateWheelPoses()
    {
        UpdateWheelPoses(frontWheelColliders[0], frontWheels[0]);
        UpdateWheelPoses(frontWheelColliders[1], frontWheels[1]);
        UpdateWheelPoses(backWheelColliders[0], backWheels[0]);
        UpdateWheelPoses(backWheelColliders[1], backWheels[1]);
    }

    void UpdateWheelPoses(WheelCollider collider, Transform transform)
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        collider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}

   
