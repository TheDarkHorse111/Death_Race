using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Suliman Abuzaid
//Anas Majdoub
//Naser Hashash
public class Player : MonoBehaviour
{
    public float health = 500f;



    public void TakeDamage(float amount) 
    {
        health -= amount;
        if (health <= 0) 
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
