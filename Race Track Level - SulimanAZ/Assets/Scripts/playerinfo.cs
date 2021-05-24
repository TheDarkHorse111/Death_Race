using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerinfo : MonoBehaviour
{
    public float health = 100f;
     public float Score = 0f;
    public void takedamge(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }




    }
    public void Die()
    {
        Destroy(gameObject);
    }


    
}
