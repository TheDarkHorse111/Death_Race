
using UnityEngine.Audio;
using UnityEngine;
 
public class Gunshooting : MonoBehaviour
{    
     public  GUN []GUNS=new GUN[2];
     private float Damge=20f;
     public float fireRate=3f;   
    //public AudioSource sound;
     private float NextTimetoFire=0f;
     public  bool CanIFire = false;
      
     public static float Timetofire= 50f;
    private void Start()
    {
      //  sound = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {  
          if (CanIFire && Timetofire>=0f)
           {
            Timetofire-=1f/ Timetofire;
            if (Input.GetButton("Fire1") && (Time.time >= NextTimetoFire))
            {
                NextTimetoFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
         if (Timetofire < 0)
         { Timetofire = 50f; CanIFire = false; }
       
    }

    void Shoot() 
    {  
        foreach(GUN i in GUNS){
            i.Effects.Play();
        FindObjectOfType<AudioManager>().Play("GUNSound");
        if (Physics.Raycast(i.Effects.transform.position, i.Effects.transform.forward, out RaycastHit hit))
        {
           
            Debug.Log(hit.transform.name);
           
            playerinfo player = hit.transform.GetComponent<playerinfo>();
            if (player != null)
            {
                player.takedamge(Damge);
            }
           
            GameObject DostroryGo =Instantiate(i.impactEffect,hit.point,Quaternion.LookRotation(hit.normal));
                     FindObjectOfType<AudioManager>().Play("bulletimpact");
            Destroy(DostroryGo,2f);
        }

        }
       
       // sound.Play();
       // FindObjectOfType<audiomaneger>().Play("anas");
    }
    private void OnCollisionEnter(Collision collision)
    {
  
    }
 
}
