using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerinfo : MonoBehaviour
{
    public float health = 100f;

    public  float DisappearingTime=40f; 
    bool readytomove=false; 
    bool flag=true; 
    float  DeathTime;
    GameObject _Shield;
    public bool CanIUseShiled=false;

    void Start()
    {   
     
    }
    void FixedUpdate()
    {  
          shiledStart();
          shieldMove(); 
          Revive();
    }
void shiledStart()
{
       if(CanIUseShiled)
        {
        
        if(flag){
            FindObjectOfType<GameManager_Script>().ObjectName("Shader",transform.position);
          _Shield=FindObjectOfType<GameManager_Script>().returnObject();
           readytomove=true;
            flag=false;
        }
        if(DisappearingTime>0)
        {
            DisappearingTime-=1f/DisappearingTime;
        }else
        {
            CanIUseShiled=false;
            DisappearingTime=40f;
            flag=true;
            readytomove=false;
            FindObjectOfType<GameManager_Script>().DestroyObject();
            Destroy(_Shield);
        }
        }
}
void shieldMove()
   {
       if(readytomove&&_Shield!=null)
       {
        _Shield.transform.position=transform.position;
       }
   }
    public void takedamge(float amount)
    {
        if(CanIUseShiled)
        {
         return;
        }
        else
        {
            health -= amount;
            if (health <= 0)
             {
            Die();
             }
        }
     

    }
    public void Die()
    {
       FindObjectOfType<AudioManager>().Play("RocketGUNImpact");
       FindObjectOfType<GameManager_Script>().ObjectName("Explosion",transform.position);
       DeathTime=Time.time;
       gameObject.SetActive(false);
      
    }
    

    public void Revive()
    {   
       if(health <=0)
       {
        if(DeathTime>Time.time+2)
        {
          Debug.Log("the Death time is : "+DeathTime);  
          Debug.Log("the Curent time is : "+Time.time+2);  
         health=100f;
         gameObject.SetActive(true);
         }
        
       } 


    }

}
