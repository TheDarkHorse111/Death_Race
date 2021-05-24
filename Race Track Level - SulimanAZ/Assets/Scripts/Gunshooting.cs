
using UnityEngine.Audio;
using UnityEngine;
 
public class Gunshooting : MonoBehaviour
{
     private float Damge=20f;
     public float fireRate=3f;
    
     
     public Camera gun;
     public ParticleSystem Effects;
     public GameObject impactEffect;
    //public AudioSource sound;
     private float NextTimetoFire=0f;
     public static bool CanIFire = false;
      
     public static float Timetofire= 50f;
    private void Start()
    {
      //  sound = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
      

          Debug.Log(Timetofire);
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
        Effects.Play();
      //  sound.Play();
       // FindObjectOfType<audiomaneger>().Play("anas");

        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out RaycastHit hit))
        {
           
            Debug.Log(hit.transform.name);
           
            playerinfo player = hit.transform.GetComponent<playerinfo>();
            if (player != null)
            {
                player.takedamge(Damge);
            }
           
            GameObject DostroryGo =Instantiate(impactEffect,hit.point,Quaternion.LookRotation(hit.normal));
             
            Destroy(DostroryGo,2f);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
         
          
    }
 
}
