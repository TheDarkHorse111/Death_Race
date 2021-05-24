using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunsactive : MonoBehaviour
{
        private void OnTriggerEnter(Collider other)
        {       
             other.GetComponent<Gunshooting>().CanIFire=true;
             gameObject.SetActive(false);
        } 

}
