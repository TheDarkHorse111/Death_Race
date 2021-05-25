using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform objectToFollow;
    public Vector3 offset;
    public float followSpeed;
    public float lookSpeed;
    // Start is called before the first frame update


    // Update is called once per frame
    private void FixedUpdate()
    {
        MoveToTarget();
        LookAtTarget();
    }

    public void LookAtTarget() 
    {
        Vector3 lookDirection = objectToFollow.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp( transform.rotation, rot, lookSpeed * Time.deltaTime);
    }
    public void MoveToTarget()
    {
        Vector3 targetPos = objectToFollow.position + objectToFollow.forward * -offset.z + objectToFollow.right * offset.x + objectToFollow.up *offset.y;
        transform.position = Vector3.Lerp( transform.position, targetPos, followSpeed + Time.deltaTime);
    }
}
