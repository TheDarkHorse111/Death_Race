using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CarController CC;
    float lastTimeMoving;
    Vector3 lastPos;
    Quaternion lastRot;
    public Text []lapcountAndPlacement;
    CheckpointManager cp;
    int carRego;

    void ResetLayer() 
    {
        CC.rb.gameObject.layer = 0;
       
        
    }


    private void Start()
    {
        CC = this.GetComponent<CarController>();
        cp = GetComponent<CheckpointManager>();
        lapcountAndPlacement = GameObject.FindObjectOfType<CanvasGroup>().GetComponentsInChildren<Text>();
        carRego = Leaderboard.RegCar(gameObject.name);
    }
    private void Update()
    {
        if (!RaceManager.racing) return;

        

        CC.GetInput();
        if (!RaceManager.racing)
        {
            CC.Transformation = 0;
            return;
        }

        if (CC.rb.velocity.magnitude > 1 || !RaceManager.racing)
            lastTimeMoving = Time.time;

        RaycastHit hit;
        if (Physics.Raycast(CC.rb.gameObject.transform.position, -Vector3.up, out hit, 10))
        {
            if (hit.collider.gameObject.tag == "Road")
            {
                lastPos = CC.rb.gameObject.transform.position;
                lastRot = CC.rb.gameObject.transform.rotation;
            }
        }

        if (Time.time > lastTimeMoving + 3 || CC.gameObject.transform.position.y < -5) 
        {
            CC.rb.gameObject.transform.position = lastPos;
            CC.rb.gameObject.transform.rotation = lastRot;
            CC.rb.gameObject.layer = 8;
            Invoke("ResetLayer", 3);
        }
        Leaderboard.setPos(carRego, cp.lap, cp.checkpoint , cp.timeEntered);
        string pos = Leaderboard.getPos(carRego);
        if (lapcountAndPlacement == null) return;
        lapcountAndPlacement[0].text = "Lap: " + cp.lap;
        lapcountAndPlacement[1].text = pos;

        CC.CheckSkidiing();
        CC.CalcEngineSound();
    }
}
