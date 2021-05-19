using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labcounter : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
         GameManager_Script.lab++; 
      
    }
   
}
