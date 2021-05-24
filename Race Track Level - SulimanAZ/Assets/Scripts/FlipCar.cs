using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCar : MonoBehaviour
{
    Rigidbody rb;
    float LastTimeChecked;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.up.y > 0.5f || rb.velocity.magnitude > 1) 
        {
            LastTimeChecked = Time.time;
        }
        if (Time.time > LastTimeChecked + 2) 
        {
            RightCar();
        }
    }

    void RightCar()
    {
        this.transform.position += Vector3.up;
        this.transform.rotation = Quaternion.LookRotation(this.transform.forward);
    }
}
