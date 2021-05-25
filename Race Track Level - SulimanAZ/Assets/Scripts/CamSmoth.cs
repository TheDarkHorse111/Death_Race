using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSmoth : MonoBehaviour
{
    public Transform target;
    public float distance = 1.6f;
    public float height = 0.4f;
    public float damping = 0.7f;
    public bool smoothRotation = true;
    public bool followBehind = true;
    public float rotationDamping = 50.0f;

    void FixedUpdate()
    {

        Vector3 wantedPosition;
        if (followBehind)
            wantedPosition = target.TransformPoint(0, height, -distance);
        else
            wantedPosition = target.TransformPoint(0, height, distance);

        transform.position = Vector3.Lerp(transform.position, wantedPosition, damping);

        if (smoothRotation)
        {
            Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        }
        else transform.LookAt(target, target.up);
    }
}
