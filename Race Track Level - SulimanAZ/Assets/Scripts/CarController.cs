using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is our code 
public class CarController : MonoBehaviour
{
    
    public WheelCollider[] frontWheelColliders = new WheelCollider[2];
    public WheelCollider[] backWheelColliders = new WheelCollider[2];
    public Transform[] frontWheels = new Transform[2];
    public Transform[] backWheels = new Transform[2];
    public float handBrakeFrictionMultiplier = 2f;
    public float maxSteeringAngle = 6f; 
    public float motorTorgue = 500f;
    public float maxbrakeTorgue = 2200f;
    public Transform COM;
    [HideInInspector]public Rigidbody rb;
    public float maxSpeed = 200f;
    
    [SerializeField]private float currentSpeed;
    private List<WheelCollider> AllWheels = new List<WheelCollider>();
    [HideInInspector]public float Transformation;
    [HideInInspector]public float rotation;
    [HideInInspector]public float Brakes;
    private bool isbraking;
    private float driftFactor;
    private float SteeringAngle = 6f;
    private WheelFrictionCurve forwardFriction, sidewaysFriction;
    
    private float radius = 6;


    public AudioSource SkidSound;
    public AudioSource HighAccel;
    public ParticleSystem smoke;
    ParticleSystem[] skidsmoke = new ParticleSystem[4];

    public GameObject LBrakeLight, RBrakeLight;

    // the time between each gear change, increase this value to make the gears change faster
    public float GearLength = 3;
    //number of gears
    public int GearNum = 5;
    // the speed between gear changes
    [HideInInspector]public float CurrentGearSpeed { get { return rb.velocity.magnitude * GearLength; } }
    // the lowest and highest pitch for the engine sound
    public float LowPitch = 1f , HighPitch = 6f;
    //the revolutions per minute
    private float RPM;
    // current gear
    private float CurrentGear = 1;
    private float CurrentGearPerc;

    


    void Start()
    {
        foreach (WheelCollider w in frontWheelColliders) 
        {
            AllWheels.Add(w);
        }
        foreach (WheelCollider w in backWheelColliders)
        {
            AllWheels.Add(w);
        }
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = COM.transform.localPosition;
        for (int i = 0; i < 4; i++) 
        {
            skidsmoke[i] = Instantiate(smoke);
            skidsmoke[i].Stop();
        }
        LBrakeLight.SetActive(false);
        RBrakeLight.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
        addDownForce();
        Steer();
        adjustTraction();
        timedLoop();
        Accelerate();
        Brake();
        UpdateWheelPoses();
        
    }
   



    public void GetInput()
    {
        Transformation = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");
        Brakes = Input.GetAxis("Jump");
        isbraking = (Input.GetAxis("Jump") != 0) ? true : false;
    }

    public void GetAiInput(float accel , float brake , float steer) 
    {  
        Transformation = Mathf.Clamp(accel, -1, 1);
        rotation = Mathf.Clamp(steer, -1, 1);
        Brakes = Mathf.Clamp(brake, 0, 1);
    }
    public void AiStarter(float accel, float brake, float steer)
    {
        
        GetAiInput(accel, brake, steer);
        CheckSkidiing();
        CalcEngineSound();
        
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
    

    void Accelerate()
    {
        foreach (WheelCollider w in backWheelColliders)
        {
            if (currentSpeed < maxSpeed)
            {
                w.motorTorque = Transformation * motorTorgue;

            }
            else 
            {
                w.motorTorque = -Transformation * motorTorgue;
            }
        }

        currentSpeed = rb.velocity.magnitude * 3.6f; ;
    }

    void Brake() 
    {
        
        foreach (WheelCollider w in backWheelColliders) 
        {

            w.brakeTorque = maxbrakeTorgue * Brakes;
            if (w.brakeTorque != 0)
            {
                LBrakeLight.SetActive(true);
                RBrakeLight.SetActive(true);
            }
            else
            {
                LBrakeLight.SetActive(false);
                RBrakeLight.SetActive(false);
            }
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
    private void adjustTraction()
    {
        //tine it takes to go from normal drive to drift 
        float driftSmothFactor = .7f * Time.deltaTime;

        if (isbraking)
        {
            sidewaysFriction = frontWheelColliders[0].sidewaysFriction;
            forwardFriction = frontWheelColliders[0].forwardFriction;

            float velocity = 0;
            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = forwardFriction.extremumValue = forwardFriction.asymptoteValue =
                Mathf.SmoothDamp(forwardFriction.asymptoteValue, driftFactor * handBrakeFrictionMultiplier, ref velocity, driftSmothFactor);

            for (int i = 0; i < 2; i++)
            {
                backWheelColliders[i].sidewaysFriction = sidewaysFriction;
                backWheelColliders[i].forwardFriction = forwardFriction;
            }
            for (int i = 0; i < 2; i++)
            {
                frontWheelColliders[i].sidewaysFriction = sidewaysFriction;
                frontWheelColliders[i].forwardFriction = forwardFriction;
            }

            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = forwardFriction.extremumValue = forwardFriction.asymptoteValue = 1.1f;
            //extra grip for the front wheels
            for (int i = 0; i < 2; i++)
            {
                frontWheelColliders[i].sidewaysFriction = sidewaysFriction;
                frontWheelColliders[i].forwardFriction = forwardFriction;
            }
            rb.AddForce(transform.forward * (currentSpeed / 400) * 40000);
        }
        //executed when handbrake is being held
        else
        {
            

            forwardFriction = frontWheelColliders[0].forwardFriction;
            sidewaysFriction = frontWheelColliders[0].sidewaysFriction;

            forwardFriction.extremumValue = forwardFriction.asymptoteValue = sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue =
                ((currentSpeed * handBrakeFrictionMultiplier) / 300) + 1;

            for (int i = 0; i < 2; i++)
            {
                backWheelColliders[i].sidewaysFriction = sidewaysFriction;
                backWheelColliders[i].forwardFriction = forwardFriction;
            }
            for (int i = 0; i < 2; i++)
            {
                frontWheelColliders[i].sidewaysFriction = sidewaysFriction;
                frontWheelColliders[i].forwardFriction = forwardFriction;
            }

        }

        //checks the amount of slip to control the drift
        for (int i = 0; i < 2; i++)
        {

            WheelHit wheelHit;

            backWheelColliders[i].GetGroundHit(out wheelHit);

            if (wheelHit.sidewaysSlip < 0) driftFactor = (1 + -rotation) * Mathf.Abs(wheelHit.sidewaysSlip);

            if (wheelHit.sidewaysSlip > 0) driftFactor = (1 + rotation) * Mathf.Abs(wheelHit.sidewaysSlip);
        }
        

    }
    private IEnumerator timedLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(.7f);
            radius = 6 + currentSpeed / 20;

        }
    }
    private void addDownForce()
    {

        rb.AddForce(-transform.up * 100f * rb.velocity.magnitude);

    }

    public void CheckSkidiing()
    {
        int numOfWheelsSkidding = 0;
        
        for (int i = 0; i < 4; i++) 
        {
            WheelHit WheelHit;
            
            AllWheels[i].GetGroundHit(out WheelHit);


            if (Mathf.Abs(WheelHit.forwardSlip) >= 0.8f || Mathf.Abs(WheelHit.sidewaysSlip) >= 0.4f)
            {
                numOfWheelsSkidding++;

                skidsmoke[i].transform.position = AllWheels[i].transform.position - AllWheels[i].transform.up * AllWheels[i].radius;
                skidsmoke[i].Emit(1);

                if (!SkidSound.isPlaying && currentSpeed >= 40)
                {
                    SkidSound.Play();
                }
                
            }
            

        } 

        if (numOfWheelsSkidding == 0 && SkidSound.isPlaying) 
        {
            SkidSound.Stop();
        }
       
    }

    public void CalcEngineSound()
    {
        float Gearperc = (1 / (float)GearNum);
        float TargetGearFactor = Mathf.InverseLerp(Gearperc * CurrentGear, Gearperc * (CurrentGear + 1), Mathf.Abs(CurrentGearSpeed / maxSpeed));

        CurrentGearPerc = Mathf.Lerp(CurrentGearPerc , TargetGearFactor , Time.deltaTime * 5f);

        float GearNumFactor = CurrentGear / (float)GearNum;
        RPM = Mathf.Lerp(GearNumFactor, 1, CurrentGearPerc);

        float SpeedPerc = Mathf.Abs(CurrentGearSpeed / maxSpeed);
        float LowerGearMax = (1 / (float)GearNum) * CurrentGear;
        float UpperGearMax = (1 / (float)GearNum) * (CurrentGear + 1 );
        if (CurrentGear > 0 && SpeedPerc < LowerGearMax) CurrentGear--;
        if ((CurrentGear < (GearNum - 1)) && SpeedPerc > UpperGearMax) CurrentGear++;

        float pitch = Mathf.Lerp(LowPitch, HighPitch, RPM);
        HighAccel.pitch = Mathf.Min(HighPitch, pitch) * 0.25f;

    }
}
    
   
