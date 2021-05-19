using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunsactive : MonoBehaviour
{
     
     

        private void OnTriggerEnter(Collider other)
        {
            Gunshooting.CanIFire = true;
             Debug.Log("Disappear");
             gameObject.SetActive(false);
        }
    
}
