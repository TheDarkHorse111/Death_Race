using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunsactive : MonoBehaviour
{
        private void OnTriggerEnter(Collider other)
        {    
       
       
        if(other.GetComponentInChildren<Gunshooting>().CanIFire==true)
        {
         return;
        }else
        {
           other.GetComponentInChildren<Gunshooting>().CanIFire=true;
             gameObject.SetActive(false);
        }
         
        } 

}
