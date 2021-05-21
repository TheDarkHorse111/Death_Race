using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playershooting : MonoBehaviour
{
    public GameObject bulletweapons;
    public Transform bulletposition1;
    public float bulletrange1 = 0.5f;
    private float numberBullet;

    public GameObject weap1;
    public GameObject weap2;
    public bool weap1avalable = true;
    public bool weap2avalable = false;
    public Transform bulletposition2;
    public float bulletrange2 = 0.1f;
    private void Start()
    {
        weap2.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (weap1avalable)
            {
                if (Time.time > numberBullet)
                {
                    numberBullet = Time.time + bulletrange1;
                    GameObject bullet = Instantiate(bulletweapons, bulletposition1.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().velocity = transform.forward * 35;
                }
            }
            if (weap2avalable) 
            {
                if (Time.time > numberBullet)
                {
                    numberBullet = Time.time + bulletrange2;
                    GameObject bullet = Instantiate(bulletweapons, bulletposition2.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().velocity = transform.forward * 35;
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.P)) 
        {
            weap1avalable = !weap1avalable;
            weap2avalable = !weap2avalable;
            weap2.SetActive(weap2avalable);
            weap1.SetActive(weap1avalable);
        }
    }
}
