using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsShooting : MonoBehaviour
{
    public GameObject bulletweapons;
    public GameObject weap1r;
    public GameObject weap2l;
    public Transform bulletposition1r;
    public Transform bulletposition2l;
    public float bulletrange1_2 = 0.5f;
    public bool weapons1_2= false;
    private float numberBullet;
    public GameObject weap3m;
    public Transform bulletposition3m;
    public float bulletrange3 = 0.1f;
    public bool weapons3=false;
    void Start()
    {
        weap1r.SetActive(false);
        weap2l.SetActive(false);
        weap3m.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            weapons1_2 = true;
            weapons3 = false;
            weap1r.SetActive(true);
            weap2l.SetActive(true);
            weap3m.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            weapons1_2 = false;
            weapons3 = true;
            weap1r.SetActive(false);
            weap2l.SetActive(false);
            weap3m.SetActive(true);
        }
        if (Input.GetKey(KeyCode.L)) 
        {
            if (weapons1_2)
            {
                if (Time.time > numberBullet)
                {
                    numberBullet = Time.time + bulletrange1_2;
                    GameObject bulletr = Instantiate(bulletweapons, bulletposition1r.position, Quaternion.identity);
                    bulletr.GetComponent<Rigidbody>().velocity = transform.forward * 35;
                    GameObject bulletl = Instantiate(bulletweapons, bulletposition2l.position, Quaternion.identity);
                    bulletl.GetComponent<Rigidbody>().velocity = transform.forward * 35;
                }
            }
            if (weapons3) 
            {
                if (Time.time > numberBullet)
                {
                    numberBullet = Time.time + bulletrange3;
                    GameObject bullet = Instantiate(bulletweapons, bulletposition3m.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().velocity = transform.forward * 35;
                }
            }
        }
    }
}
