using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject [] countdownItems;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in countdownItems)
            g.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
