 using UnityEngine.Audio;
 using UnityEngine;
 using System;

public class AudioManager :MonoBehaviour 
{
    public Sound[] Sounds ;
    
     
    void Awake()
     {
          

        foreach(Sound s in Sounds)
        {
            s.Source=gameObject.AddComponent<AudioSource>();
            s.Source.clip=s.Clip;
            s.Source.pitch=s.pitch;
            s.Source.volume=s.volume;
            s.Source.spatialBlend=1.0f;
            s.Source.minDistance=30f;
             s.Source.maxDistance=500f;
            s.Source.loop=s.loop;            
        }


     }


     public void Play(string Name)
     {
       Sound s =Array.Find(Sounds,Sound=>Sound.Name==Name);  
            if(s==null)
            {
              Debug.Log("the sound "+Name+" is not found");
                return;

            }
          
            s.Source.Play();


     }
}
