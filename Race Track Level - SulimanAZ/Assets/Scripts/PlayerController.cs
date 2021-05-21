using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CarController CC;


    private void Start()
    {
        CC = this.GetComponent<CarController>();
    }
    private void Update()
    {
        CC.GetInput();
        CC.CheckSkidiing();
        CC.CalcEngineSound();
    }
}
