using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public int lap = 0;
    public int checkpoint = -1;

    int checkpointCount;
    int nextCheckpoint;
    public GameObject lastCP;
    // Start is called before the first frame update
    void Start()
    {
        checkpointCount = GameObject.FindGameObjectsWithTag("Checkpint").Length;
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
                if (checkpoint == 0)
                    lap++;
                nextCheckpoint++;
                if (nextCheckpoint >= checkpointCount)
                    nextCheckpoint = 0;
            }
        }
    }
}
