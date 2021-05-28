using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController: MonoBehaviour
{
    public RaceTrackWP RTWP;
    public float brakingSensitivity = 3f;
    CarController cc;
    public float steeringSensitivity = 0.01f;
    public float accelSensitivity = 0.3f;
    Vector3 target;
    Vector3 nextTarget;
    int currentWP = 0;
    float totalDistanceToTarget;
    public Text[] lapcountAndPlacement;
    GameObject tracker;
    int currentTrackerWP = 0;
    float lookAhead = 12;
    float lastTimeMoving = 0;
    int carRego;

    CheckpointManager cpm;

    private void Awake()
    {
        RTWP = FindObjectOfType<RaceTrackWP>();
    }

    void Start()
    {
        cc = this.GetComponent<CarController>();
        cc.rb = gameObject.GetComponent<Rigidbody>();
        target = RTWP.Waypoints[currentWP].transform.position;
        nextTarget = RTWP.Waypoints[currentWP + 1].transform.position;
        totalDistanceToTarget = Vector3.Distance(target, cc.rb.gameObject.transform.position);

        tracker = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = cc.rb.gameObject.transform.position;
        tracker.transform.rotation = cc.rb.gameObject.transform.rotation;

        this.GetComponent<Ghost>().enabled = false;
        lapcountAndPlacement = GameObject.FindObjectOfType<CanvasGroup>().GetComponentsInChildren<Text>();
        carRego = Leaderboard.RegCar(gameObject.name);

    }


    void ProgressTracker()
    {
        Debug.DrawLine(cc.rb.gameObject.transform.position, tracker.transform.position);

        if (Vector3.Distance(cc.rb.gameObject.transform.position, tracker.transform.position) > lookAhead) return;

        tracker.transform.LookAt(RTWP.Waypoints[currentTrackerWP].transform.position);
        tracker.transform.Translate(0, 0, 1.0f);  //speed of tracker

        if (Vector3.Distance(tracker.transform.position, RTWP.Waypoints[currentTrackerWP].transform.position) < 1)
        {
            currentTrackerWP++;
            if (currentTrackerWP >= RTWP.Waypoints.Length)
                currentTrackerWP = 0;
        }

    }

    void ResetLayer() 
    {
        cc.rb.gameObject.layer = 0;
        this.GetComponent<Ghost>().enabled = false;
    }
   
    void Update()
    {
        if(!RaceManager.racing)
        {
            lastTimeMoving = Time.time;
            return;
        }
        ProgressTracker();
        Vector3 localTarget;
        if (cc.rb.velocity.magnitude > 1)
            lastTimeMoving = Time.time;
        if (cpm == null)
            cpm = GetComponent<CheckpointManager>();

        if (Time.time > lastTimeMoving + 4 || cc.gameObject.transform.position.y < -5)
        {
            cc.rb.gameObject.transform.position = cpm.lastCP.transform.position + Vector3.up * 2;
            cc.rb.gameObject.transform.rotation = cpm.lastCP.transform.rotation;
            tracker.transform.position = cpm.lastCP.transform.position;
            
            Vector3 relativePos = tracker.transform.position - transform.position;

            if (Vector3.Dot(transform.forward, relativePos) < 0.0f)
                tracker.transform.position = cc.rb.gameObject.transform.position + -Vector3.forward;

         
            
            cc.rb.gameObject.layer = 8;
            this.GetComponent<Ghost>().enabled = true;
            Invoke("ResetLayer", 3);
        }

            
        if (Time.time < cc.rb.GetComponent<AvoidDetector>().avoidTime)
            localTarget = tracker.transform.right * cc.rb.GetComponent<AvoidDetector>().avoidPath;
        else
            localTarget = cc.rb.gameObject.transform.InverseTransformPoint(tracker.transform.position);


        float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        float steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1, 1);
        float brake = 0;
        float accel = 1f;

        float SpeedFactor = cc.CurrentGearSpeed / cc.maxSpeed;
        float Corner = Mathf.Clamp(Mathf.Abs(targetAngle),0,90);
        float CornerFactor = Corner / 90.0f;

        if (Corner > 10 && SpeedFactor > 0.1)
            brake = Mathf.Lerp(0, 1 + SpeedFactor * brakingSensitivity, CornerFactor);

        if (Corner > 20)
            accel = Mathf.Lerp(0,1 + accelSensitivity, 1 - CornerFactor);

        cc.AiStarter(accel, brake, steer);
        Leaderboard.setPos(carRego, cpm.lap, cpm.checkpoint, cpm.timeEntered);
        string pos = Leaderboard.getPos(carRego);
        if (lapcountAndPlacement == null) return;
        lapcountAndPlacement[0].text = "Lap: " + cpm.lap;
        lapcountAndPlacement[1].text = pos;


    }
}