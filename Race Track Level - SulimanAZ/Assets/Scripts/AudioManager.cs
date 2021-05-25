 using UnityEngine.Audio;
 using UnityEngine;
 using System;

public class AudioManager :MonoBehaviour 
{
    public Sound[] Sounds ;
    public static AudioManager instance;
     
    void Awake()
     {
         if(instance==null)
         {
           instance=this;

         }else
         {
             Destroy(gameObject);
             return;
         }
       
          DontDestroyOnLoad(gameObject);

        foreach(Sound s in Sounds)
        {
            s.Source=gameObject.AddComponent<AudioSource>();
            s.Source.clip=s.Clip;
            s.Source.pitch=s.pitch;
            s.Source.volume=s.volume;
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
