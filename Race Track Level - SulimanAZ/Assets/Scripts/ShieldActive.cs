using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActive : MonoBehaviour
{      
    private void OnTriggerEnter(Collider other)
    {    
              gameObject.SetActive(false);              
    }

 
}
