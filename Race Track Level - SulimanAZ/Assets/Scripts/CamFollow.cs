using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
    Vector3 Velocity = Vector3.one;
    void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    private void HandleRotation()
    {
        var dirction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(dirction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleTranslation()
    {
        var targetPostion = target.TransformPoint(offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPostion, ref Velocity, translateSpeed * Time.deltaTime);
    }
}
