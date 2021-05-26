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


    public Object_IN[] Objects;
    Object_IN _Gameobject;
    GameObject _Shield;





      public static int lab = 1;
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
      labcounter();
    }



   public void ObjectName(String Name,Vector3 Player)
   {
      _Gameobject= Array.Find(Objects,Object=>Object.Name==Name);
      _Shield=Instantiate(_Gameobject._GameObject1,Player, Quaternion.identity);
   }
   public GameObject returnObject()
   {
 
       return _Shield;
       
   }
   public void DestroyObject( )
   {
      Destroy(_Shield);
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
                      }
                    if(DisappearingTime>0)
                      {
                            DisappearingTime-=1f/DisappearingTime;
                      }else
                      {
                            DisappearingTime=40f;
                            flag=true;
                            i.SetActive(true);
                            break;
                      }
                  }

                } 
     }
   void labcounter()
   {

       // if(lab<3)
       //  textlab.text="lab :"+lab.ToString();
        // else { textlab.text="( ͡❛ ‿‿ ͡❛)";}

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
