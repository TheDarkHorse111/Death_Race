using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunsactive : MonoBehaviour
{
        private void OnTriggerEnter(Collider other)
        {    
       
       
        if(other.GetComponent<Gunshooting>().CanIFire==true)
        {
         return;
        }else
        {
             other.GetComponent<Gunshooting>().CanIFire=true;
             gameObject.SetActive(false);
        }
         
        } 

}
