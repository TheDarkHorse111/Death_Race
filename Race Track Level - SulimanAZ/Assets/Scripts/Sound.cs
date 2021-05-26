﻿ using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound  {
public string Name;
public AudioClip Clip;
[Range(0f,1f)]
public float volume;
[Range(.1f,3f)]
public float pitch;
//[Range(0f,1f)]
///public float SpatialBlend;
   public bool loop;

[HideInInspector]
 public AudioSource Source;
}
