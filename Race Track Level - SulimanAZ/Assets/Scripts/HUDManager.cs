using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{

    CanvasGroup CanvasGroup;
    
    void Start()
    {
        CanvasGroup = this.GetComponent<CanvasGroup>();
        CanvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (RaceManager.racing) CanvasGroup.alpha = 1;
    }
}
