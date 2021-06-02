using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public GameObject [] countdownItems;
    public static bool racing = false;
    public static int totalLaps = 0;
    public GameObject gameOverPanel;
    public GameObject HUD;
    GameObject[] cars;
    GameObject[] AiCars;
    CheckpointManager[] carsCPM;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in countdownItems)
            g.SetActive(false);
        StartCoroutine(PlayerCountdown());
        gameOverPanel.SetActive(false);
        cars = GameObject.FindGameObjectsWithTag("AI");

        carsCPM = new CheckpointManager[cars.Length+1];

        for (int i = 0; i < cars.Length; i++) 
        {
            carsCPM[i] = cars[i].GetComponent<CheckpointManager>();
        }
        carsCPM[cars.Length] = GameObject.FindGameObjectWithTag("Player").GetComponent<CheckpointManager>();

    }

    IEnumerator PlayerCountdown() 
    {
        yield return new WaitForSeconds(2);
        foreach (GameObject g in countdownItems)
        {
            g.SetActive(true);
            yield return new WaitForSeconds(1);
            g.SetActive(false);
        }
        racing = true;
    }
   
    void LateUpdate()
    {
        int finishedCount = 0;
        foreach (CheckpointManager cpm in carsCPM) 
        {
            if (cpm.lap == totalLaps + 1)
                finishedCount++;

            if (finishedCount == cars.Length)
            {
                HUD.SetActive(false);
                gameOverPanel.SetActive(true);
            }
        }
    }
}
