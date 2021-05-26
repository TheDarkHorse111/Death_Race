using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActive : MonoBehaviour
{      
    private void OnTriggerEnter(Collider other)
    {         
              
        if( other.GetComponent<playerinfo>().CanIUseShiled==true)
        {
         return;
        }else
        {
           other.GetComponent<playerinfo>().CanIUseShiled=true;    
           gameObject.SetActive(false);
        }
    }

 
}
