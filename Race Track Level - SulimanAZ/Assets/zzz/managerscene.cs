using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class managerscene : MonoBehaviour
{
    private string NameScenes;
    public GameObject rotatecube;
    public float speedrotate;
    public carslist listCar;
    public int carpointer=0;
    public Vector3 locationcar;
    private GameObject cc;

    public carslist listCarRocketL;
    public static bool list1MG;
    public static bool list2RL;
    
    public carslist listcarscriptGun;
    public carslist listcarscriptRocketL;

    private void Start()
    {
        NameScenes = StaticName.SceneName;
    }
    private void Awake()
    {
        list1MG = true; list2RL = false;

        PlayerPrefs.SetInt("pointer", 0);
        carpointer = PlayerPrefs.GetInt("pointer");
        //GameObject carload = Instantiate(listCar.car[carpointer], locationcar, Quaternion.identity)as GameObject;
        //carload.transform.parent = rotatecube.transform;
        cc=Instantiate(listCar.car[carpointer], locationcar, Quaternion.identity) as GameObject;
        ////cc.GetComponents<Carcontrol2>();
    }
    private void FixedUpdate()
    {
        rotatecube.transform.Rotate(Vector3.up * speedrotate * Time.deltaTime);
        cc.transform.Rotate(Vector3.up * speedrotate * Time.deltaTime);
    }
    public void NextCar()
    {
        if (list1MG)
        {
            if (carpointer < listCar.car.Length - 1)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                carpointer++;
                PlayerPrefs.SetInt("pointer", carpointer);
                cc = Instantiate(listCar.car[carpointer], locationcar, Quaternion.identity) as GameObject;
            }
        }
        else if(list2RL)
        {
            if (carpointer < listCarRocketL.car.Length - 1)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                carpointer++;
                PlayerPrefs.SetInt("pointer", carpointer);
                cc = Instantiate(listCarRocketL.car[carpointer], locationcar, Quaternion.identity) as GameObject;
            }
        }
    }
    public void PreviousCar()
    {
        if (carpointer > 0 && list1MG)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            carpointer--;
            PlayerPrefs.SetInt("pointer", carpointer);
            cc = Instantiate(listCar.car[carpointer], locationcar, Quaternion.identity) as GameObject;
        }
        else if (carpointer > 0 && list2RL)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            carpointer--;
            PlayerPrefs.SetInt("pointer", carpointer);
            cc = Instantiate(listCarRocketL.car[carpointer], locationcar, Quaternion.identity) as GameObject;
        }
    }
    public void StartGame() 
    {
        //StaticName.CarName = cc;
        StaticName.pc = carpointer;
        if (list1MG) { StaticName.cpc = listcarscriptGun; }
        else { StaticName.cpc = listcarscriptRocketL; }
        SceneManager.LoadScene(NameScenes);
    }
    public void MachineGun() 
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        cc = Instantiate(listCar.car[carpointer], locationcar, Quaternion.identity) as GameObject;
        list1MG = true; list2RL = false;
    }

    public void RocketLauncher() 
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        cc = Instantiate(listCarRocketL.car[carpointer], locationcar, Quaternion.identity) as GameObject;
        list1MG = false; list2RL = true;  
    }

    

}
