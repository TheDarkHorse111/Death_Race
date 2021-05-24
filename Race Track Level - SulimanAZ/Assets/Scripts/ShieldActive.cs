using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActive : MonoBehaviour
{      
    private void OnTriggerEnter(Collider other)
    {      
               Debug.Log(other.name);
              GameManager_Script.PlayerName=other.name;
              gameObject.SetActive(false);              
    }

 
}
