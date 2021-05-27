
using UnityEngine.Audio;
using UnityEngine;
 
public class Gunshooting : MonoBehaviour
{   
    internal enum GUNType{
        RocketGUN,
        MachineGUN,
    }
      internal enum PlayerType{
        AI,
        human,
    }
    [SerializeField]private GUNType guntype;
    [SerializeField]private PlayerType playertype;
     public  GUN []GUNS=new GUN[2];
     private float Damge=20f;
     public float fireRate=3f;   
    //public AudioSource sound;
        public float ImpactForce=60f;
     private float NextTimetoFire=0f;
     public  bool CanIFire = false;
      
     public static float Timetofire= 50f;
    private void Start()
    {
     
    }
    // Update is called once per frame
    void FixedUpdate()
    {  
        

    if (CanIFire && Timetofire>=0f&&playertype==PlayerType.AI)
    {  
      
        Debug.Log("AI shooting his Name :"+transform.name);
        AiShoot();

    }else if(CanIFire && Timetofire>=0f&&playertype==PlayerType.human)
    {   
        Debug.Log("Player shooting his Name :"+transform.name);
         playershoot();

    }
      
        
        if (Timetofire < 0)
        { 
             Timetofire = 50f;
             CanIFire = false;
        }
       
    }

    void Shoot() 
    {  
        foreach(GUN i in GUNS){

        i.Effects.Play();
        if(guntype==GUNType.MachineGUN){  FindObjectOfType<AudioManager>().Play("MachineGUNSound");}
        else if(guntype==GUNType.RocketGUN) {FindObjectOfType<AudioManager>().Play("RocketGUNSound");} 
        if (Physics.Raycast(i.Aim.transform.position, i.Aim.transform.forward,out RaycastHit hit))
        {
           
         
           
          
            playerinfo player = hit.transform.GetComponentInChildren<playerinfo>();
        //      Debug.Log(hit.transform.name);
            if (player != null)
            {
               if(guntype==GUNType.MachineGUN) player.takedamge(Damge+20f);
               else player.takedamge(Damge);
            }

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);

            GameObject DostroryGo =Instantiate(i.impactEffect,hit.point,Quaternion.LookRotation(hit.normal));
            Destroy(DostroryGo,2f);
            if(guntype==GUNType.MachineGUN){FindObjectOfType<AudioManager>().Play("MachineGUNImpact");}
            else if(guntype==GUNType.RocketGUN) {FindObjectOfType<AudioManager>().Play("RocketGUNImpact");}

        }

        }
       
       
    }
 
    void AiShoot()
    {
            Timetofire-=1f/ Timetofire;
            if ( (Time.time >= NextTimetoFire))
            {
                NextTimetoFire = Time.time + 1f / fireRate;
                Shoot();
            } 
    }

   void playershoot()
   {
            Timetofire-=1f/ Timetofire;
            if (Input.GetButton("Fire1") && (Time.time >= NextTimetoFire))
            {
                NextTimetoFire = Time.time + 1f / fireRate;
                Shoot();
            }   
   }
}
