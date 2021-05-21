using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{

    public Transform car;
    public Vector3 xyz;
    public float transspeed;
    public float rotationspeed;
    public bool camerafollow = true;
    public Vector3 Frontxyz;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            camerafollow = !camerafollow;
        }
        if (camerafollow)
        {
            //transform.position = car.position + xyz;
            //Quaternion rota = Quaternion.LookRotation(-xyz);
            //transform.rotation = rota;

            //new
            Vector3 targetlocation = car.TransformPoint(xyz);
            transform.position = Vector3.Lerp(transform.position, targetlocation, transspeed * Time.deltaTime);
            Vector3 direction = car.position - transform.position;
            Quaternion rota = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rota, rotationspeed * Time.deltaTime);
        }
        else 
        {
            //transform.position = car.position + Frontxyz;
            Vector3 targetlocation = car.TransformPoint(Frontxyz);
            transform.position = Vector3.Lerp(transform.position, targetlocation, transspeed * Time.deltaTime);
            Vector3 direction = transform.position - car.position;
            Quaternion rota = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rota, rotationspeed * Time.deltaTime);
        }
    }
}
