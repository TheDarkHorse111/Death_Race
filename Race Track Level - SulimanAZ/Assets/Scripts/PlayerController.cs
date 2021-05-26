﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CarController CC;
    float lastTimeMoving;
    Vector3 lastPos;
    Quaternion lastRot;


    void ResetLayer() 
    {
        CC.rb.gameObject.layer = 0;
    }


    private void Start()
    {
        CC = this.GetComponent<CarController>();
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

        if (Time.time > lastTimeMoving + 5) 
        {
            CC.rb.gameObject.transform.position = lastPos;
            CC.rb.gameObject.transform.rotation = lastRot;
            CC.rb.gameObject.layer = 8;
            Invoke("ResetLayer", 3);
        }
        CC.CheckSkidiing();
        CC.CalcEngineSound();
    }
}
