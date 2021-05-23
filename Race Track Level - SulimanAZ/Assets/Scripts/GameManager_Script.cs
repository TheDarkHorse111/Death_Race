using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Script : MonoBehaviour
{
    public GameObject []Points;
    public GameObject myPrefabShield;
    public GameObject myPrefabGun;
    public GameObject sheder;
     GameObject player;
    static public int lab = 1;
    public Text textlab; 

      bool readytomove=false; 
      bool flag=true; 
     List<GameObject> myPrefabShieldlist;
     List<GameObject> myPrefabGunlist;
    float DisappearingTime=40f; 
    
    // Start is called before the first frame update
    void Start()
    {   
      initialize();
    }

    // Update is called once per frame

        
  void FixedUpdate()
    {  
      GunsManager();
      ShieldsManager();
      shieldMove();
     // labcounter();
    }
   void GunsManager() 
   {
       foreach(GameObject i in myPrefabGunlist)
                {
                  
                  if(i.activeSelf.Equals(false))
                  { 
                      if(Gunshooting.Timetofire<=1)
                      {
                         i.SetActive(true);
                         break;
                      }                  
                          
                    
                    
                  }
                }
   } 
   void ShieldsManager()
     {
                  foreach(GameObject i in myPrefabShieldlist)
                  {
                  
                  if(i.activeSelf.Equals(false))
                  { 
                      if(flag)
                      { 
                         
                          player=Instantiate(sheder,GameObject.Find("Free_Racing_Car_Gray").transform.position, Quaternion.identity);
                          readytomove=true;
                          flag=false;
                      }
                    if(DisappearingTime>0)
                      {
                            DisappearingTime-=1f/DisappearingTime;
                            
                      
                      }else
                      {
                            DisappearingTime=40f;
                            flag=true;
                            readytomove=false;
                            Destroy(player);
                            i.SetActive(true);
                            break;
                      }
                    
                  }

                } 
     }
   void labcounter()
   {

        if(lab<3)
         textlab.text="lab :"+lab.ToString();
         else { textlab.text="( ͡❛ ‿‿ ͡❛)";}

   }
   void shieldMove()
   {
       if(readytomove)
       {
        player.transform.position= GameObject.Find("Free_Racing_Car_Gray").transform.position;
       }
   }
     
  void initialize()
  {
        myPrefabShieldlist=new List<GameObject>();
        myPrefabGunlist=new List<GameObject>();
        foreach(GameObject i in Points )
        {      
          GameObject _Shield= Instantiate(myPrefabShield,i.transform.position, Quaternion.identity);
          GameObject _Gun=Instantiate(myPrefabGun,i.transform.position+new Vector3(10,0,6), Quaternion.identity);
        
          myPrefabShieldlist.Add(_Shield);
          myPrefabGunlist.Add(_Gun);
        }


  }

}
