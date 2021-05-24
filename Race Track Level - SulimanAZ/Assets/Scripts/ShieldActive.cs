using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActive : MonoBehaviour
{      
    private void OnTriggerEnter(Collider other)
    {         
               other.GetComponent<playerinfo>().CanIUseShiled=true;
               gameObject.SetActive(false);              
    }

 
}
