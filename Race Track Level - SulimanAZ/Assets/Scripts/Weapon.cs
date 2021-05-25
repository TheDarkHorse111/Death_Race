using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Suliman Abuzaid
//Anas Majdoub
//Naser Hashash

public class Weapon : MonoBehaviour
{
    public float damage;
    public float range;
    public float ImpactForce;
    public float FireRate = 8f;
    private float NextTimeToFire = 0f;
    public ParticleSystem muzzleflash;
    public GameObject ImpactEffect;
    public GameObject barrel;
    

    
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire) 
        {
            NextTimeToFire = Time.time + 1f / FireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
       
        muzzleflash.Play();

        RaycastHit hit;
        if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit)) 
        {
           
            
           Player p = hit.transform.GetComponent<Player>();
            if( p != null)
               p.TakeDamage(damage);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);

            GameObject impactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactGO, 1f);


        }
    }
}
