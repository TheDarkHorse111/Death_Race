using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    public int lap = -1;
    public int checkpoint = -1;
    int checkpointCount;
    int nextCheckpoint;
    public float timeEntered = 0;
    public GameObject lastCP;
    GameObject[] cps;
    // Start is called before the first frame update
    void Start()
    {
        cps = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpointCount = cps.Length;

        foreach(GameObject c in cps)
        {
            if (c.name == "0")
            {
                lastCP = c;
                break;
            }
        }    

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            int thisCPNum = int.Parse(other.gameObject.name);
            if (thisCPNum == nextCheckpoint) 
            {
                lastCP = other.gameObject;

                checkpoint = thisCPNum;
                timeEntered = Time.time;
                if (checkpoint == cps.Length-1)
                    lap++;

                nextCheckpoint++;
                if (nextCheckpoint >= checkpointCount)
                    nextCheckpoint = 0;
            }
        }
    }
}
