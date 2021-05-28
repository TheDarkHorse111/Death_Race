using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMA : MonoBehaviour
{
    ////public GameObject carplay;
    //public Vector3 locationcarPlayer;
    public GameObject[] pointerlocation;
    public int playerlocation=4;
    public carslist listcarAIGun;
    public carslist listcarAIRocket;
    ////public carslist listcarR;
    public int p;
    public carslist typecarp;
    public GameObject c;
    public GameObject[] AICars;
    
    void Awake()
    {
        int random =Random.Range(0,2);
        typecarp = StaticName.cpc;
        p = StaticName.pc;
        c = Instantiate(typecarp.car[p],pointerlocation[playerlocation].transform.position, pointerlocation[playerlocation].transform.rotation);
        //c.GetComponent<CarController>();
        //carplay = StaticName.CarName;
        ////GameObject carplayf = Instantiate(carplay, locationc, Quaternion.identity)as GameObject;
        for (int i = 0; i < pointerlocation.Length; i++)
        {
            if (i == playerlocation) { continue; }
            else if (i == 1 || i == 3 || i == 5)
            {
                AICars[i] = Instantiate(listcarAIRocket.car[random], pointerlocation[i].transform.position, pointerlocation[i].transform.rotation);
            }
            else 
            {
                if (i == playerlocation) { AICars[i] = gameObject; }
                else
                {
                    AICars[i] = Instantiate(listcarAIGun.car[random], pointerlocation[i].transform.position, pointerlocation[i].transform.rotation);
                }
            }
        }
    }

    
}
